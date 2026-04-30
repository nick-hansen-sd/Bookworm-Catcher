using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    //Moises---------------
    [SerializeField] private Button musicVolumeButton;
    [SerializeField] private Button soundEffectVolumeButton;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI soundEffectVolumeText;
    [SerializeField] private int mainMenuSceneIndex = 0;
    [SerializeField] private int gameplaySceneIndex = 3;

    private void Awake()
    {
       // Check if buttons are assigned to avoid the error
        if (soundEffectVolumeButton != null) {
            soundEffectVolumeButton.onClick.AddListener(() => {
                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.ChangeVolume();
                }
                UpdateVisual();
            });
        }

        if (musicVolumeButton != null) {
            musicVolumeButton.onClick.AddListener(() => {
                if (MusicManager.Instance != null)
                {
                    MusicManager.Instance.ChangeVolume();
                }
                UpdateVisual();
            });
        }

        //Update the visual of the volume buttons
        UpdateVisual();
    }

    private void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (soundEffectVolumeText != null && SoundManager.Instance != null)
        {
            soundEffectVolumeText.text = "Sound FX: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        }

        if (musicVolumeText != null && MusicManager.Instance != null)
        {
            musicVolumeText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
        }
    }
    //----------------------
    
    // Use this for the Options button opened from Main Menu.
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(mainMenuSceneIndex);  
    }

    // Use this for the Options button opened from Pause Menu.
    public void BackToPausedGame()
    {
        Time.timeScale = 1f;
        PausedMenu.ReturnToPausedStateOnLoad = true;
        SceneManager.LoadSceneAsync(gameplaySceneIndex);
    }
}
