using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    [SerializeField] private int mainMenuSceneIndex = 0;

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

    public void QuitGame()
    {
        Application.Quit();
    }
}
