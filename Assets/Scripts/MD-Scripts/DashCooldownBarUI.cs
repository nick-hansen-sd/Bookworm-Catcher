using TMPro;
using UnityEngine;
using UnityEngine.UI; // Crucial: This allows us to use the 'Image' type

public class DashCooldownBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage; 
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private PlayerRefactor playerRefactor;

    private void Update()
    {
        if (!TryResolvePlayerRefactor())
        {
            ApplyProgress(1f);
            return;
        }

        UpdateFromPlayer(
            playerRefactor.IsDashActive(), 
            playerRefactor.IsDashReady(), 
            playerRefactor.GetDashDuration(), 
            playerRefactor.GetDashTimeRemaining(),
            playerRefactor.GetDashCooldownDuration(), 
            playerRefactor.GetDashCooldownTimeRemaining()
        );
    }

    private bool TryResolvePlayerRefactor()
    {
        if (playerRefactor != null) return true;

        playerRefactor = PlayerRefactor.Instance != null ? PlayerRefactor.Instance : FindFirstObjectByType<PlayerRefactor>();
        return playerRefactor != null;
    }

    private void UpdateFromPlayer(bool isDashing, bool isDashReady, float dashDuration, float dashTimeRemaining, float cooldownDuration, float cooldownTimeRemaining)
    {
        if (isDashing)
        {
            float dashProgress = dashDuration > 0f ? Mathf.Clamp01(dashTimeRemaining / dashDuration) : 0f;
            ApplyProgress(dashProgress);
            if (statusText != null)
            {
                statusText.text = "DASH " + dashTimeRemaining.ToString("0.0") + "s";
            }
            return;
        }

        float cooldownProgress = cooldownDuration > 0f ? 1f - Mathf.Clamp01(cooldownTimeRemaining / cooldownDuration) : 1f;
        ApplyProgress(cooldownProgress);

        if (statusText != null)
        {
            statusText.text = isDashReady ? "DASH READY" : "CD " + cooldownTimeRemaining.ToString("0.0") + "s";
        }
    }

    private void ApplyProgress(float progress)
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = Mathf.Clamp01(progress);
        }
    }
}