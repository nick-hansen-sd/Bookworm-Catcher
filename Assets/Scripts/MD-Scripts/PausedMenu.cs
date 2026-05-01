using UnityEngine;
using UnityEngine.InputSystem;

public class PausedMenu : MonoBehaviour
{
    public static bool ReturnToPausedStateOnLoad { get; set; }

    [SerializeField] private GameObject pauseContainer;
    private bool hasShownMissingReferenceWarning;

    private void Start()
    {
        if (ReturnToPausedStateOnLoad)
        {
            ReturnToPausedStateOnLoad = false;

            if (pauseContainer != null)
            {
                Transform pauseRoot = pauseContainer.transform.parent;
                if (pauseRoot != null)
                {
                    pauseRoot.SetAsLastSibling();
                }

                pauseContainer.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (pauseContainer == null)
            {
                if (!hasShownMissingReferenceWarning)
                {
                    Debug.LogWarning("Pause container is not assigned in the Inspector on PausedMenu.");
                    hasShownMissingReferenceWarning = true;
                }

                return;
            }

            bool shouldPause = !pauseContainer.activeSelf;
            if (shouldPause)
            {
                Transform pauseRoot = pauseContainer.transform.parent;
                if (pauseRoot != null)
                {
                    pauseRoot.SetAsLastSibling();
                }
            }

            pauseContainer.SetActive(shouldPause);
            Time.timeScale = shouldPause ? 0f : 1f;
        }
    }

}
