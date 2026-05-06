using System;
using UnityEngine;
using UnityEngine.Serialization;

public class DepositBox : MonoBehaviour, IBookwormParent
{
    public event EventHandler OnBookwormDeposited;
    
    [SerializeField] private Transform bookwormHoldPoint;

    //---------MARI-------------------------
    [SerializeField] private int pointsPatrolWorm = 100;
    [SerializeField] private int pointsSabotageWorm = 150;
    [FormerlySerializedAs("pointsPerBookworm")]
    [SerializeField] private int pointsPerBookwormFallback = 100;
    //--------------------------------------

    private Bookworm _bookworm;

    //---------MARI-------------------------
    //Warning for missing ScoreSystem
    private bool _hasShownMissingScoreSystemWarning;
    //--------------------------------------


    public Transform GetBookwormTransform()
    {
        return bookwormHoldPoint;
    }

    public void SetBookworm(Bookworm bookworm)
    {
        _bookworm = bookworm;

        if (_bookworm != null)
        {

            //---------MARI-------------------------
            //Add points to score system
            ScoreSystem scoreSystem = ScoreSystem.Instance != null ? ScoreSystem.Instance : FindObjectOfType<ScoreSystem>();

            if (scoreSystem == null && !_hasShownMissingScoreSystemWarning)
            {
                Debug.LogWarning("ScoreSystem not found in scene. Bookworms will not award points.");
                _hasShownMissingScoreSystemWarning = true;
            }

            scoreSystem?.RegisterCaughtWorm(GetPointsForBookworm(_bookworm));
            //--------------------------------------
    
            OnBookwormDeposited?.Invoke(this, EventArgs.Empty);

            //---------MARI-------------------------
            //Destroy bookworm once deposited
            _bookworm.DestroySelf();
            //--------------------------------------
            
        }
    }

    public Bookworm GetBookworm()
    {
        return _bookworm;
    }

    public void ClearBookworm()
    {
        _bookworm = null;
    }

    public bool HasBookworm()
    {
        return _bookworm != null;
    }

    //---------MARI-------------------------
    private int GetPointsForBookworm(Bookworm bookworm)
    {
        if (bookworm == null)
        {
            return pointsPerBookwormFallback;
        }

        if (bookworm.GetComponent<WormAttackLadder>() != null)
        {
            return pointsSabotageWorm;
        }

        if (bookworm.GetComponent<WormPatrol>() != null)
        {
            return pointsPatrolWorm;
        }

        return pointsPerBookwormFallback;
    }
    //--------------------------------------
}
