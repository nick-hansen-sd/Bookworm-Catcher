using UnityEngine;
using UnityEngine.InputSystem;

public class AC_PlayerAnimationRefactor : MonoBehaviour
{
    [SerializeField] private PlayerRefactor playerRefactor;
    [SerializeField] private Animator animator;
    private string currentAnim;
    private string currentBaseAnim;


    private void Start()
    {
        animator = GetComponent<Animator>();
        currentAnim = "FacingFront";
    }
    

    private void Update()
    {
        if (playerRefactor.GetState() == PlayerRefactor.State.Dashing)
        {
            if (0 != playerRefactor.GetMovementX())
            {
                if (0 > playerRefactor.GetMovementX())
                    ActionAnimation("DashLeft", "FacingLeft");
                
                else if (0 < playerRefactor.GetMovementX())
                    ActionAnimation("DashRight", "FacingRight");
            }
            
        }
        
        
        else if(playerRefactor.GetState() == PlayerRefactor.State.Moving || 0 != playerRefactor.GetMovementX())
        {        
            // if (player.GetState() != Player.State.SingleJump && player.GetState() != Player.State.DoubleJump)
            // {
                if (0 > playerRefactor.GetMovementX())
                    ActionAnimation("WalkingLeft", "FacingLeft");
                
                else if (0 < playerRefactor.GetMovementX())
                    ActionAnimation("WalkingRight", "FacingRight");
            // }

            // else
            // {
            //     animator.SetBool("WalkingRight", false);
            //     animator.SetBool("WalkingLeft", false);
            // }
            
        }


        if (playerRefactor.GetState() == PlayerRefactor.State.Idle || 0 == playerRefactor.GetMovementX())
        {
            animator.SetBool("WalkingRight", false);
            animator.SetBool("WalkingLeft", false);
            animator.SetBool("DashRight", false);
            animator.SetBool("DashLeft", false);
            
            if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame )
            {
                SwitchBaseAnimation("FacingBack");
            }
                
            if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame )
            {
                SwitchBaseAnimation("FacingFront");
            }
                
            if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame )
            {
                SwitchBaseAnimation("FacingLeft");
            }
                
            if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame )
            {
                SwitchBaseAnimation("FacingRight");
            }
        }
        
        
                
    }

    private void SwitchBaseAnimation(string newAnim)
    {
        if (newAnim != currentAnim)
        {
            animator.SetBool(currentAnim, false);
            animator.SetBool(currentBaseAnim, false);

            animator.SetBool(newAnim, true);
            
            currentAnim = newAnim;
            currentBaseAnim = newAnim;
            // Debug.Log(currentAnim);
        }
    }

    private void ActionAnimation(string newAnim, string newBaseAnim)
    {
        if (newAnim != currentAnim)
        {
            animator.SetBool(currentAnim, false);
            animator.SetBool(currentBaseAnim, false);

            animator.SetBool(newBaseAnim, true);
            animator.SetBool(newAnim, true);

            currentAnim = newAnim;
            currentBaseAnim = newBaseAnim;
        }
    }

}

