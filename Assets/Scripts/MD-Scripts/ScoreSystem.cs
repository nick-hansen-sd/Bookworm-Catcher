using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance { get; private set; }
    public event EventHandler OnScoreChanged;

    [SerializeField] private int startingScore = 0;

    public int Score { get; private set; }
    public int CaughtWormCount { get; private set; }
    public int TotalWormCount { get; private set; }
    public int RemainingWormCount { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        Score = startingScore;
    }

    private void NotifyChange() => OnScoreChanged?.Invoke(this, EventArgs.Empty);

    public void AddPoints(int pointsToAdd)
    {
        if (pointsToAdd <= 0) return;

        Score += pointsToAdd;
        NotifyChange();
    }

    public void RegisterCaughtWorm(int pointsPerWorm)
    {
        if (RemainingWormCount <= 0 || pointsPerWorm <= 0) return;

        CaughtWormCount++;
        RemainingWormCount = Mathf.Max(0, RemainingWormCount - 1);
        Score += pointsPerWorm;
        
        NotifyChange();
    }

    public void SetWormCount(int total)
    {
        TotalWormCount = Mathf.Max(0, total);
        RemainingWormCount = TotalWormCount;
        CaughtWormCount = 0;
        Score = startingScore;
        NotifyChange();
    }

    public void ResetScore()
    {
        Score = startingScore;
        CaughtWormCount = 0;
        RemainingWormCount = TotalWormCount;
        NotifyChange();
    }
}