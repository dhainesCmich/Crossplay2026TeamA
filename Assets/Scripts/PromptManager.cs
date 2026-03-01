using UnityEngine;

public class PromptManager : MonoBehaviour
{
    public static PromptManager instance;
    public GameObject promptPrefab;
    private GameObject promptCanvas;

    private void Awake()
    {
        instance = this;
        if(promptPrefab)
            promptCanvas = Instantiate(promptPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
    
    public GameObject getPrompt() {
        return promptCanvas;
    }
}
