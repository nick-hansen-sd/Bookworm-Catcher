using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldownBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage; 
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private PlayerRefactor player;

    [Header("Colors")]
    [SerializeField] private Color notReadyColor = Color.red;
    [SerializeField] private Color readyColor = new Color(0.854902f, 0.6470588f, 0.1254902f, 1f); 

    private void Update()
    {
        if (!EnsurePlayerReference())
        {
            ApplyUI(1f, "NO PLAYER", isReady: true);
            return;
        }

        if (player.IsDashActive())
        {
            float dashProgress = player.GetDashDuration() > 0 ? player.GetDashTimeRemaining() / player.GetDashDuration() : 0;
            // Yellow while actively dashing.
            ApplyUI(dashProgress, $"DASH {player.GetDashTimeRemaining():0.0}s", isReady: true);
        }
        else if (!player.IsDashReady())
        {
            float cdProgress = player.GetDashCooldownDuration() > 0 ? 1f - (player.GetDashCooldownTimeRemaining() / player.GetDashCooldownDuration()) : 1f;
            // Red while refilling / on cooldown.
            ApplyUI(cdProgress, $"CD {player.GetDashCooldownTimeRemaining():0.0}s", isReady: false);
        }
        else
        {
            ApplyUI(1f, "DASH READY", isReady: true);
        }
    }

    private bool EnsurePlayerReference()
    {
        if (player != null) return true;
        player = PlayerRefactor.Instance != null ? PlayerRefactor.Instance : FindFirstObjectByType<PlayerRefactor>();
        return player != null;
    }

    private void ApplyUI(float progress, string text, bool isReady)
    {
        if (fillImage)
        {
            fillImage.fillAmount = Mathf.Clamp01(progress);
            fillImage.color = isReady ? readyColor : notReadyColor;
        }
        if (statusText) statusText.SetText(text);
    }
}