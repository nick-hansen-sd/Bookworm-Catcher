using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject pauseContainer;
    [SerializeField] private int mainMenuSceneIndex = 0;
    [SerializeField] private int optionsSceneIndex = 8;
    [SerializeField] private string pausedSettingsSceneName = "PausedSettingsButton";

    public void ResumeGame()
    {
        if (pauseContainer != null)
        {
            pauseContainer.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void OpenMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(mainMenuSceneIndex);
    }

    public void OpenSettings()
    {
        Time.timeScale = 1f;

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
