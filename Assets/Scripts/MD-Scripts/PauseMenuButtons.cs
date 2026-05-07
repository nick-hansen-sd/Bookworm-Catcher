using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject pauseContainer;
    [SerializeField] private int mainMenuSceneIndex = 0;
    [SerializeField] private int optionsSceneIndex = 1;
    [SerializeField] private string pausedSettingsSceneName = "PausedSettingsButton";

    public void ResumeGame()
    {
        if (pauseContainer != null)
        {
            pauseContainer.SetActive(false);
        }

        Time.timeScale = 1f;

        //Moises
        AudioListener.pause = false;
    }

    public void OpenMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(mainMenuSceneIndex);

        //Moises
        AudioListener.pause = false;
    }

    public void OpenSettings()
    {

        Time.timeScale = 1f;
        OptionsMenu.SetReturnGameplaySceneIndex(SceneManager.GetActiveScene().buildIndex);

         //Moises
        AudioListener.pause = false;
        
        if (!string.IsNullOrWhiteSpace(pausedSettingsSceneName))
        {
            SceneManager.LoadSceneAsync(pausedSettingsSceneName);
            return;
        }

        SceneManager.LoadSceneAsync(optionsSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
