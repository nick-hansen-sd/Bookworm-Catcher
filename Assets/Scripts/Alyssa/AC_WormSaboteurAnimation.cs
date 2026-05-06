using NUnit.Framework;
using UnityEngine;

public class AC_WormSaboteurAnimation : MonoBehaviour
{
    [SerializeField] private WormAttackLadder wormSaboteur;
    [SerializeField] private Animator animator;
    private string IS_CAUGHT = "isCaught";
    private string IS_ATTACK = "isAttacking";
    private string ATTACK_ACTIVE = "AttackActive";


    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_CAUGHT, false);
        animator.SetBool(IS_ATTACK, false);
    }


    private void Update()
    {
        Debug.Log("Worm State: " + wormSaboteur.GetState());
        
        //defualt wiggle state of the worm 
        if (wormSaboteur.GetState() == WormAttackLadder.StateMachine.Search)
            animator.SetBool(IS_ATTACK, false);
        
        //plays attack animation and checks when it can play item drop animation 
        if (wormSaboteur.GetState() == WormAttackLadder.StateMachine.Attack)
        {
            animator.SetBool(IS_ATTACK, true);
            TriggerAttackAnim(wormSaboteur.IsAttackNow());
        }
    
        //changes to caught animation when worm is caught
        if (wormSaboteur.GetState() == WormAttackLadder.StateMachine.Caught)
            animator.SetBool(IS_CAUGHT, true);
            
        //change animation speed when worm gets faster when retreating, resets speed afterwards 
        if (wormSaboteur.GetState() == WormAttackLadder.StateMachine.Retreat)
        {
            animator.SetBool(IS_ATTACK, false);
            animator.speed = wormSaboteur.GetSpeedModifier();
        }
        else
            animator.speed = 1f;
        
    }


    private void TriggerAttackAnim(bool attackNow)
    {
        if (attackNow)
        {
            //only fires animation for that specific worm 
            SpriteRenderer spriteRender = gameObject.GetComponentInChildren<SpriteRenderer>();
            if (spriteRender != this.gameObject.GetComponent<SpriteRenderer>())
                return;
            
            //plays attack animation once, returns to attack waiting after 
            animator.SetTrigger(ATTACK_ACTIVE);
        }
        
    }

}
