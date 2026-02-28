using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction m_moveAction;
    private InputAction m_jumpAction;
    
    private InputAction m_pauseActionPlayer;
    private InputAction m_pauseActionUI;

    private Vector2 m_moveAmt;
    private Rigidbody2D m_rigidbody2d;
    private Animator m_animator;

    public float WalkSpeed = 5;
    public float JumpSpeed = 5; 

    public GameObject PauseDisplay;
    public GameObject InventoryUI;

    private InputAction m_inventorySwitch;

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

        m_rigidbody2d = GetComponent<Rigidbody2D>();
        //m_animator = GetComponent<Animator>;

        m_pauseActionPlayer = InputSystem.actions.FindAction("Player/Pause");
        m_pauseActionUI = InputSystem.actions.FindAction("UI/Pause");

        m_inventorySwitch = InputSystem.actions.FindAction("Player/InventoryAction");
    }

    // Update is called once per frame
    private void Update()
    {
        m_moveAmt = m_moveAction.ReadValue<Vector2>();

        if (m_jumpAction.WasPressedThisFrame()) {
            Jump();
        }

        DisplayPause();
    }

    private void FixedUpdate()
    {
        Walking();
    }

    private void Walking() {
        Debug.Log(m_moveAmt.x);
        Debug.Log("transform.right = " + transform.right);
        m_rigidbody2d.AddForce(transform.right * m_moveAmt.x * WalkSpeed * Time.deltaTime);
    }

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

    public void Jump() {
        m_rigidbody2d.AddForceAtPosition(new Vector2(0, 5f), Vector2.up, ForceMode2D.Impulse);
    }
}
