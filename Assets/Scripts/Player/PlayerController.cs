using System.Collections;
using System.Collections.Generic;
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
    private Vector2 m_moveAmt;
    private Animator m_animator;

    [Header("Player Settings")]
    public float WalkSpeed = 5;
    public float JumpSpeed = 5; 

    [Header("Grounding")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    private float horizontal;

    [Header("Outside Objects")]
    public GameObject PauseDisplay;
    public GameObject InventoryUI;

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
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * WalkSpeed, rb.linearVelocity.y);
    }
    
    #region PLAYER_CONTROLS
    public void Move(InputAction.CallbackContext context)
    {
       horizontal = context.ReadValue<Vector2>().x;
    }
    
    public void Jump(InputAction.CallbackContext context) 
    {
        if (context.performed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpSpeed);    
        }
        else if (context.performed)
        {
            Debug.Log("No ground!"); 
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
    #endregion

    private void DisplayPause()
    {
        if (m_pauseActionPlayer.WasPressedThisFrame())
        {
            PauseDisplay.SetActive(true);
            InputActions.FindActionMap("Player").Disable();
            InputActions.FindActionMap("UI").Enable();
        }
        else if (m_pauseActionUI.WasPressedThisFrame())
        {
            PauseDisplay.SetActive(false);
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
	}
}
