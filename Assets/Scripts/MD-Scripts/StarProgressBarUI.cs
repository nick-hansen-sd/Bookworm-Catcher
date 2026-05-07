using System;
using UnityEngine;
using UnityEngine.UI;

public class StarProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private ScoreSystem _boundSystem;
    private bool _loggedFillHint;

    private void OnEnable()
    {
        TryBindAndRefresh();
    }

    private void Start()
    {
        TryBindAndRefresh();
    }

    private void Update()
    {
        if (_boundSystem == null)
        {
            TryBindAndRefresh();
        }
    }

    private void OnDisable()
    {
        if (_boundSystem != null)
        {
            _boundSystem.OnScoreChanged -= OnScoreChanged;
            _boundSystem = null;
        }
    }

    private void OnScoreChanged(object sender, EventArgs e) => Refresh();

    private void TryBindAndRefresh()
    {
        if (_boundSystem != null)
        {
            Refresh();
            return;
        }

        ScoreSystem s = ScoreSystem.Instance != null
            ? ScoreSystem.Instance
            : FindAnyObjectByType<ScoreSystem>();
        if (s == null)
        {
            return;
        }

        _boundSystem = s;
        _boundSystem.OnScoreChanged += OnScoreChanged;

        WarnIfFillImageMisconfigured();

        Refresh();
    }

    private void WarnIfFillImageMisconfigured()
    {
        if (_loggedFillHint || fillImage == null || fillImage.type == Image.Type.Filled)
        {
            return;
        }

        Debug.LogWarning(
            "StarProgressBarUI: the Fill Image must use Image Type \"Filled\" (Horizontal) so fillAmount affects the bar. " +
            "Assign the front fill stripe, not the background.",
            fillImage);
        _loggedFillHint = true;
    }

    private void Refresh()
    {
        var score = _boundSystem != null ? _boundSystem : ScoreSystem.Instance;
        if (score == null)
        {
            return;
        }

        int total = score.TotalWormCount;
        int caught = score.CaughtWormCount;

        float progress = total > 0 ? (float)caught / total : 0f;

        if (fillImage != null)
        {
            fillImage.fillAmount = Mathf.Clamp01(progress);
        }
    }
}
