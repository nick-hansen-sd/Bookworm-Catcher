using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private float SceneTransitionTimer = 31f; // Timer until credits transitions to main menu

    void Update()
    {
        SceneTransitionTimer -= Time.deltaTime;

        if (SceneTransitionTimer <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
