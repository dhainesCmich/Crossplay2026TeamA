using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [Header("Interaction Settings")]
    public string interactPrompt = "Press E to interact";
    public float interactionRadius = 1.5f;
    public PromptManager promptManager;
    private GameObject promptCanvas;
    protected bool playerInRange = false;
    protected Transform player;
    protected virtual void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }
    public virtual void Interact()
    {
        // if (promptCanvas != null)
        //         promptCanvas.SetActive(false);
        Debug.Log("Interacting with " + gameObject.name);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // if (!promptCanvas)
            //     promptCanvas = promptManager.getPrompt();
            // if (promptCanvas != null)
            //     promptCanvas.SetActive(true);
            Debug.Log("Player in range of object!");
            playerInRange = true;
            player = other.transform;   
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // if (!promptCanvas)
            //     promptCanvas = promptManager.getPrompt();
            // if (promptCanvas != null)
            //     promptCanvas.SetActive(false);
            playerInRange = false;
            player = null;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}