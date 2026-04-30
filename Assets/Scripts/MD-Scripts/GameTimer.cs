using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";
    private const int PointsPerSecondRemaining = 50;

    [SerializeField] private float startTimeSeconds = 60f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private string timerPrefix = "TIME: ";

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject[] filledStars;
    [SerializeField] private GameObject winTitleImage;
    [SerializeField] private GameObject gameOverTitleImage;
    [SerializeField] private TMP_Text gameOverScoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private string gameOverScorePrefix = "YOUR SCORE: ";
    [SerializeField] private string highScorePrefix = "HIGH SCORE: ";

    private float _timeRemaining;
    private bool _hasEnded;

    private void Start()
    {
        // Ensure gameplay starts unpaused when scene loads.
        Time.timeScale = 1f;
        _timeRemaining = Mathf.Max(0f, startTimeSeconds);

        LevelParser levelParser = FindFirstObjectByType<LevelParser>();
        ScoreSystem scoreSystem = ScoreSystem.Instance != null ? ScoreSystem.Instance : FindFirstObjectByType<ScoreSystem>();
        if (levelParser != null && scoreSystem != null && levelParser.wormCountGetter() > 0)
        {
            scoreSystem.SetWormCount(levelParser.wormCountGetter());
        }

        //Make sure the game over screen is not active
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        UpdateGameOverScoreText(startingScore: true, finalScore: 0);
        UpdateTimerText();
    }

    private void Update()
    {
        if (_hasEnded)
        {
            return;
        }

        if (AreAllWormsCaught())
        {
            _hasEnded = true;
            UpdateTimerText();
            GameOver();
            return;
        }

        _timeRemaining -= Time.deltaTime;
        if (_timeRemaining <= 0f)
        {
            _timeRemaining = 0f;
            _hasEnded = true;
            UpdateTimerText();
            GameOver();
            return;
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if (timerText == null)
        {
            return;
        }

        int wholeSeconds = Mathf.CeilToInt(_timeRemaining);
        timerText.text = timerPrefix + wholeSeconds;
    }

    private bool AreAllWormsCaught()
    {
        ScoreSystem scoreSystem = ScoreSystem.Instance != null ? ScoreSystem.Instance : FindFirstObjectByType<ScoreSystem>();
        return scoreSystem != null && scoreSystem.GetTotalWormCount() > 0 && scoreSystem.GetRemainingWormCount() <= 0;
    }

    private void GameOver(){
        Time.timeScale = 0f;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        ScoreSystem scoreSystem = ScoreSystem.Instance != null ? ScoreSystem.Instance : FindFirstObjectByType<ScoreSystem>();
        int caughtWorms = scoreSystem != null ? scoreSystem.GetCaughtWormCount() : 0;
        int totalWorms = scoreSystem != null ? scoreSystem.GetTotalWormCount() : 0;

        int scoreBeforeTimeBonus = scoreSystem != null ? scoreSystem.GetScore() : 0;
        bool caughtAllWorms = totalWorms > 0 && caughtWorms >= totalWorms;
        int timeBonus = caughtAllWorms ? Mathf.Max(0, Mathf.FloorToInt(_timeRemaining)) * PointsPerSecondRemaining : 0;
        if (scoreSystem != null && timeBonus > 0)
        {
            scoreSystem.AddPoints(timeBonus);
        }

        int finalScore = scoreBeforeTimeBonus + timeBonus;
        UpdateGameOverScoreText(startingScore: false, finalScore: finalScore);

        // Reset filled star overlays so only earned stars are shown.
        if (filledStars != null)
        {
            foreach (GameObject star in filledStars)
            {
                if (star != null)
                {
                    star.SetActive(false);
                }
            }
        }

        bool isWin = totalWorms > 0 && caughtWorms * 2 >= totalWorms;

        if (isWin){
            // WIN STATE
            if (winTitleImage != null) winTitleImage.SetActive(true);
            if (gameOverTitleImage != null) gameOverTitleImage.SetActive(false);

            AwardStar(caughtWorms, totalWorms);
        }else{
            // LOSE STATE
            if (winTitleImage != null) winTitleImage.SetActive(false);
            if (gameOverTitleImage != null) gameOverTitleImage.SetActive(true);
        }
    }

    private void AwardStar(int caughtWorms, int totalWorms){
        if (filledStars == null || filledStars.Length == 0)
        {
            return;
        }

        if (totalWorms <= 0)
        {
            return;
        }

        if (filledStars.Length > 0 && filledStars[0] != null && caughtWorms * 2 >= totalWorms)
        {
            filledStars[0].SetActive(true);
        }

        if (filledStars.Length > 1 && filledStars[1] != null && caughtWorms * 4 >= totalWorms * 3)
        {
            filledStars[1].SetActive(true);
        }

        if (filledStars.Length > 2 && filledStars[2] != null && caughtWorms >= totalWorms)
        {
            filledStars[2].SetActive(true);
        }
    }

    private void UpdateGameOverScoreText(bool startingScore, int finalScore)
    {
        int displayedScore = startingScore ? 0 : finalScore;

        int savedHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        int updatedHighScore = Mathf.Max(savedHighScore, displayedScore);

        if (!startingScore && updatedHighScore > savedHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, updatedHighScore);
            PlayerPrefs.Save();
        }

        if (gameOverScoreText != null)
        {
            gameOverScoreText.text = gameOverScorePrefix + displayedScore.ToString("D5");
        }

        if (highScoreText != null)
        {
            highScoreText.text = highScorePrefix + updatedHighScore.ToString("D5");
        }
    }

}

