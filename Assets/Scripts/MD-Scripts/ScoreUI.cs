using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private string scorePrefix = "SCORE: ";

    private void Awake()
    {
        // Null-coalescing assignment: if scoreText is null, try to get component
        scoreText ??= GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        // Directly subscribe to the Singleton instance
        if (ScoreSystem.Instance != null)
        {
            ScoreSystem.Instance.OnScoreChanged += UpdateScoreText;
            UpdateScoreText();
        }
    }

    private void OnDisable()
    {
        if (ScoreSystem.Instance != null)
        {
            ScoreSystem.Instance.OnScoreChanged -= UpdateScoreText;
        }
    }

    private void UpdateScoreText(object sender, System.EventArgs e) => UpdateScoreText();

    private void UpdateScoreText()
    {
        if (scoreText == null) return;

        int score = ScoreSystem.Instance != null ? ScoreSystem.Instance.Score : 0;
        scoreText.SetText($"{scorePrefix}{score}");
    }
}