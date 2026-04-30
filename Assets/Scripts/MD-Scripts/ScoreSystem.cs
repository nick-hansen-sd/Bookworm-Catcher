using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance { get; private set; }
    public event EventHandler OnScoreChanged;

    [SerializeField] private int startingScore = 0;
    private int _score;
    private int _caughtWormCount;
    private int _totalWormCount;
    private int _remainingWormCount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("There are multiple instances of ScoreSystem.");
            return;
        }

        Instance = this;
        _score = startingScore;
    }

    public int GetScore()
    {
        return _score;
    }

    public int GetCaughtWormCount()
    {
        return _caughtWormCount;
    }

    public int GetTotalWormCount()
    {
        return _totalWormCount;
    }

    public int GetRemainingWormCount()
    {
        return _remainingWormCount;
    }

    public void AddPoints(int pointsToAdd)
    {
        if (pointsToAdd <= 0)
        {
            return;
        }

        _score += pointsToAdd;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RegisterCaughtWorm(int pointsPerWorm)
    {
        if (_remainingWormCount <= 0 || pointsPerWorm <= 0)
        {
            Debug.LogWarning("Worm catch ignored. Remaining=" + _remainingWormCount + ", pointsPerWorm=" + pointsPerWorm + ", total=" + _totalWormCount);
            return;
        }

        _caughtWormCount++;
        _remainingWormCount = Mathf.Max(0, _remainingWormCount - 1);
        _score += pointsPerWorm;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);

        Debug.Log("Worm caught: " + _caughtWormCount + "/" + _totalWormCount + " caught, " + _remainingWormCount + " left.");
    }

    public void SetWormCount(int totalWormCount)
    {
        _totalWormCount = Mathf.Max(0, totalWormCount);
        _remainingWormCount = _totalWormCount;
        _caughtWormCount = 0;
        _score = startingScore;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetScore(int newScore)
    {
        _score = Mathf.Max(0, newScore);
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ResetScore()
    {
        _score = startingScore;
        _caughtWormCount = 0;
        _remainingWormCount = _totalWormCount;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }
}
