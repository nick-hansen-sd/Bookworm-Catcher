using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    [SerializeField] private int mainMenuSceneIndex = 0;
    [SerializeField] private int creditsSceneIndex = 8;

    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OpenMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(mainMenuSceneIndex);
    }

    public void OpenNextLevel()
    {
        Time.timeScale = 1f;

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex >= 0 && nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(nextSceneIndex);
            return;
        }

        // Fallback: if there is no next scene in build settings, return to menu.
        SceneManager.LoadSceneAsync(mainMenuSceneIndex);
    }

    public void OpenCredits()
    {
        SceneManager.LoadSceneAsync(creditsSceneIndex);
    }
}
