using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Player Actions")]
    public InputActionAsset InputActions;
    private InputAction m_moveAction;
    private InputAction m_jumpAction;
    private InputAction m_pauseActionPlayer;
    private InputAction m_pauseActionUI;

    private InputAction m_inventorySwitch;

    [Header("Component Pieces")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private BoxCollider2D playerCollider;
    private Vector2 m_moveAmt;
    private Animator m_animator;

    [Header("Player Settings")]
    public float WalkSpeed = 5;
    public float JumpSpeed = 5; 
    [Header("Jump Tuning")]
    public float JumpHoldForce = 10f;
    public float JumpHoldTime = 0.5f;
    public float FallGravityMultiplier = 2.5f;
    private bool isJumping;
    private float jumpTimeCounter;

    [Header("Grounding")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    private bool isFacingRight = true;

    private float horizontal;

    [Header("Outside Objects")]
    public GameObject PauseDisplay;
    public GameObject InventoryUI;
    public Animator effectsAnimator;
    public AudioSource playerSounds;

    void Start() 
    {
        m_animator = GetComponent<Animator>();
        effectsAnimator.gameObject.SetActive(false);
        transform.position = PlayerSpawnManager.spawnPosition;
    }

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();  
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake() {
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_jumpAction = InputSystem.actions.FindAction("Jump");

        //m_animator = GetComponent<Animator>;

        m_pauseActionPlayer = InputSystem.actions.FindAction("Player/Pause");
        m_pauseActionUI = InputSystem.actions.FindAction("UI/Pause");

        m_inventorySwitch = InputSystem.actions.FindAction("Player/InventoryAction");
    }

    // Update is called once per frame
    private void Update()
    {
        DisplayPause();
        FlipSprite();
        JumpHold();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * WalkSpeed, rb.linearVelocity.y);
        m_animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        m_animator.SetBool("isJumping", !IsGrounded());
    }
    
    #region PLAYER_CONTROLS
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontal = input.x;

        // Drop through platform
        if (input.y < -0.5f && IsGrounded() && !IsFloor())
        {
            StartCoroutine(DropThroughPlatform());
        }
    }
    
    public void Jump(InputAction.CallbackContext context) 
    {
        if (context.performed && IsGrounded())
        {
            isJumping = true;
            playerSounds.Play();
            jumpTimeCounter = JumpHoldTime;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpSpeed);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
    private bool IsFloor()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer, 2f, 3f);
    }
    #endregion

    void FlipSprite()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void DisplayPause()
    {
        if (m_pauseActionPlayer.WasPressedThisFrame())
        {
            PauseDisplay.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            InputActions.FindActionMap("Player").Disable();
            InputActions.FindActionMap("UI").Enable();
        }
        else if (m_pauseActionUI.WasPressedThisFrame())
        {
            PauseDisplay.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            InputActions.FindActionMap("UI").Disable();
            InputActions.FindActionMap("Player").Enable();
        }

        if (m_inventorySwitch.WasPressedThisFrame())
		{
			SwitchInventory();  
		}
    }

    public void SwitchInventory()
	{
		InventoryUI.SetActive(!InventoryUI.activeSelf);
        if (InventoryUI.activeSelf)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
	}
    public void JumpHold()
    {
        //Variable jump height based on how long the player holds the jump button
        if (isJumping && m_jumpAction.IsPressed())
        {
            if (jumpTimeCounter > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpHoldForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (m_jumpAction.WasReleasedThisFrame())
        {
            isJumping = false;
        }
        // Better falling
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity * (FallGravityMultiplier - 1) * Time.deltaTime;
        }
    }
    private IEnumerator DropThroughPlatform()
    {
        playerCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        playerCollider.enabled = true;
    }
}
