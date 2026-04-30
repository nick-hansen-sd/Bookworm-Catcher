using UnityEngine;
using UnityEngine.InputSystem;

public class AC_PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Player player;
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
        if (player.GetState() == Player.State.Dashing)
        {
            if (0 != player.GetMovementX())
            {
                if (0 > player.GetMovementX())
                    ActionAnimation("DashLeft", "FacingLeft");
                
                else if (0 < player.GetMovementX())
                    ActionAnimation("DashRight", "FacingRight");
            }
            
        }
        
        
        else if(player.GetState() == Player.State.Moving || 0 != player.GetMovementX())
        {        
            // if (player.GetState() != Player.State.SingleJump && player.GetState() != Player.State.DoubleJump)
            // {
                if (0 > player.GetMovementX())
                    ActionAnimation("WalkingLeft", "FacingLeft");
                
                else if (0 < player.GetMovementX())
                    ActionAnimation("WalkingRight", "FacingRight");
            // }

            // else
            // {
            //     animator.SetBool("WalkingRight", false);
            //     animator.SetBool("WalkingLeft", false);
            // }
            
        }


        if (player.GetState() == Player.State.Idle || 0 == player.GetMovementX())
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
