using UnityEngine;

public class AC_WormSaboteurAnimation : MonoBehaviour
{
    [SerializeField] private WormAttackLadder wormSaboteur;
    [SerializeField] private Animator animator;
    private string IS_CAUGHT = "isCaught";


    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_CAUGHT, false);
    }


    private void Update()
    {
        // Debug.Log("Worm State: " + wormDefault.GetState());
        
        //changes to caught animation when worm is caught 
        if (wormSaboteur.GetState() == WormAttackLadder.StateMachine.Caught)
        {
            animator.SetBool(IS_CAUGHT, true);
        }
    }
}
