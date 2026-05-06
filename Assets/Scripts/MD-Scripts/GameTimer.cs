using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";
    private const int PointsPerSec = 50;

    [SerializeField] private float startTimeSeconds = 60f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private string timerPrefix = "TIME: ";

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winTitle;
    [SerializeField] private GameObject loseTitle;
    [SerializeField] private GameObject[] filledStars;

    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private TMP_Text highScoreText;

    private float _timeRemaining;
    private bool _isPaused;

    private ScoreSystem Score => ScoreSystem.Instance;

    private void Start()
    {
        Time.timeScale = 1f;
        _timeRemaining = startTimeSeconds;

        ResolveEndTitles();
        
        var parser = FindFirstObjectByType<LevelParser>();
        if (parser != null && Score != null)
            Score.SetWormCount(parser.wormCountGetter());

        if (gameOverScreen) gameOverScreen.SetActive(false);
        UpdateTimerText();
    }

    private void Update()
    {
        if (_isPaused) return;

        _timeRemaining -= Time.deltaTime;

        if (Score != null && Score.TotalWormCount > 0 && Score.RemainingWormCount <= 0)
            EndGame(true);
        else if (_timeRemaining <= 0)
            EndGame(HasReachedWinThreshold());

        UpdateTimerText();
    }

    private bool HasReachedWinThreshold()
    {
        if (Score == null || Score.TotalWormCount <= 0)
        {
            return false;
        }

        // Win if player caught at least half of the worms.
        return Score.CaughtWormCount * 2 >= Score.TotalWormCount;
    }

    private void UpdateTimerText()
    {
        if (timerText) timerText.SetText($"{timerPrefix}{Mathf.CeilToInt(Mathf.Max(0, _timeRemaining))}");
    }

    private void EndGame(bool won)
    {
        _isPaused = true;
        Time.timeScale = 0f;
        if (gameOverScreen) gameOverScreen.SetActive(true);

        ResolveEndTitles();

        int finalScore = CalculateFinalScore(won);
        HandleHighscore(finalScore);
        UpdateEndUI(won, finalScore);
    }

    private int CalculateFinalScore(bool won)
    {
        if (Score == null) return 0;
        
        int timeBonus = won ? Mathf.FloorToInt(Mathf.Max(0, _timeRemaining)) * PointsPerSec : 0;
        if (timeBonus > 0) Score.AddPoints(timeBonus);
        
        return Score.Score;
    }

    private void UpdateEndUI(bool won, int finalScore)
    {
        if (winTitle) winTitle.SetActive(won);
        if (loseTitle) loseTitle.SetActive(!won);

        gameOverScoreText?.SetText($"YOUR SCORE: {finalScore:D5}");
        
        for (int i = 0; i < filledStars.Length; i++)
        {
            if (filledStars[i] == null) continue;
            
            bool earned = i switch {
                0 => Score.CaughtWormCount * 2 >= Score.TotalWormCount,
                1 => Score.CaughtWormCount * 4 >= Score.TotalWormCount * 3,
                2 => Score.CaughtWormCount >= Score.TotalWormCount,
                _ => false
            };
            filledStars[i].SetActive(earned);
        }
    }

    private void HandleHighscore(int currentScore)
    {
        int high = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (currentScore > high)
        {
            high = currentScore;
            PlayerPrefs.SetInt(HighScoreKey, high);
            PlayerPrefs.Save();
        }
        highScoreText?.SetText($"HIGH SCORE: {high:D5}");
    }

    private void ResolveEndTitles()
    {
        if (gameOverScreen == null)
        {
            return;
        }

        if (winTitle == null)
        {
            Transform win = gameOverScreen.transform.Find("YouWinTitle");
            if (win == null) win = gameOverScreen.transform.Find("WinTitle");
            if (win != null) winTitle = win.gameObject;
        }

        if (loseTitle == null)
        {
            Transform lose = gameOverScreen.transform.Find("GameOverTitle");
            if (lose == null) lose = gameOverScreen.transform.Find("LoseTitle");
            if (lose == null) lose = gameOverScreen.transform.Find("TryAgainTitle");
            if (lose != null) loseTitle = lose.gameObject;
        }
    }
}
