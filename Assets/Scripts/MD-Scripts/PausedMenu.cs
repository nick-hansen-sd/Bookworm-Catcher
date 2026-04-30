using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PausedMenu : MonoBehaviour
{
    public static bool ReturnToPausedStateOnLoad { get; set; }

    [SerializeField] private GameObject pausedMenu;
    [SerializeField] private int mainMenuSceneIndex = 0;
    [SerializeField] private int optionsSceneIndex = 8;
    [SerializeField] private string pausedSettingsSceneName = "PausedSettingsButton";
    private bool hasShownMissingReferenceWarning;

    private void Start()
    {
        if (ReturnToPausedStateOnLoad)
        {
            ReturnToPausedStateOnLoad = false;

            if (pausedMenu != null)
            {
                pausedMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (pausedMenu == null)
            {
                if (!hasShownMissingReferenceWarning)
                {
                    Debug.LogWarning("PausedMenu is not assigned in the Inspector on PausedMenu.");
                    hasShownMissingReferenceWarning = true;
                }

                return;
            }

            pausedMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    //Resumes the game
    public void ResumeGame(){
        if (pausedMenu == null)
        {
            if (!hasShownMissingReferenceWarning)
            {
                Debug.LogWarning("PausedMenu is not assigned in the Inspector on PausedMenu.");
                hasShownMissingReferenceWarning = true;
            }

            return;
        }

        pausedMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    
    //Goes to the main menu scene
   public void OpenMainMenu(){
    Time.timeScale = 1f;
    SceneManager.LoadSceneAsync(mainMenuSceneIndex);
   }

    //Goes to the options menu scene
   public void OpenSettings(){
    Time.timeScale = 1f;

    if (!string.IsNullOrWhiteSpace(pausedSettingsSceneName))
    {
        SceneManager.LoadSceneAsync(pausedSettingsSceneName);
        return;
    }

    SceneManager.LoadSceneAsync(optionsSceneIndex);
   }

    //Restarts the current level
    public void RestartLevel()
    {
        //Sets the timeScale back to 1, otherwise the restarted game will be frozen
        Time.timeScale = 1f; 
        
        //This gets the name of whatever level scene we are currently in and reloads it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Closes the game
   public void QuitGame(){
    Application.Quit();
   }
}
