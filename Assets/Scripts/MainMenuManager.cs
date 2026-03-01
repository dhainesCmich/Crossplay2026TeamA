using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [Header("Main Menu")]
    public GameObject MainMenuCanvas;
    public Button NewGameButton;
    public Button CreditsButton;
    
    [Header("Scenes")]
    public string NewGameScene;

    [Header("Credits")]
    public GameObject CreditsCanvas;
    public Button ReturnToMenuButton;

    // Button listeners
    void Start()
    {
        if(NewGameButton)
            NewGameButton.onClick.AddListener(StartNewGame);

        ShowMainMenu();
    }

    // Update is called once per frame
    void StartNewGame()
    {
        if (NewGameScene != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene(NewGameScene);
        }
    }

    void ShowCredits()
    {
        MainMenuCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }   

    void ShowMainMenu() 
    {
        CreditsCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }
}
