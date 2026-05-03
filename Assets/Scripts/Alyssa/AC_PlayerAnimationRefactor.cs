using UnityEngine;
using UnityEngine.InputSystem;

public class AC_PlayerAnimationRefactor : MonoBehaviour
{
    [SerializeField] private PlayerRefactor playerRefactor;
    [SerializeField] private Animator animator;
    private string currentAnim;
    private string currentBaseAnim;

    private string FACING_FRONT = "FacingFront";
    private string FACING_BACK = "FacingBack";
    private string FACING_LEFT = "FacingLeft";
    private string FACING_RIGHT = "FacingRight";
    private string WALKING_RIGHT = "WalkingRight";
    private string WALKING_LEFT = "WalkingLeft";
    private string DASH_RIGHT = "DashRight";
    private string DASH_LEFT = "DashLeft";


    private void Start()
    {
        //base animations are necessary so that when an action stops (i.e. walking), it goes to it's matching idle rather than a mismatch one

        animator = GetComponent<Animator>();
        currentAnim = FACING_FRONT;
        currentBaseAnim = FACING_FRONT;
    }
    

    private void Update()
    {
        // Debug.Log(playerRefactor.GetState());
        
        if (playerRefactor.GetState() == PlayerRefactor.State.Dashing)
        {
            if (0 != playerRefactor.GetMovementX())
            {
                if (0 > playerRefactor.GetMovementX())
                    SwitchAnimation(DASH_LEFT, FACING_LEFT);
                
                else if (0 < playerRefactor.GetMovementX())
                    SwitchAnimation(DASH_RIGHT, FACING_RIGHT);
            }
            
        }
        
        
        if(playerRefactor.GetState() == PlayerRefactor.State.Moving)
        {    
            if (0 > playerRefactor.GetMovementX())
                SwitchAnimation(WALKING_LEFT, FACING_LEFT);
            
            else if (0 < playerRefactor.GetMovementX())
                SwitchAnimation(WALKING_RIGHT, FACING_RIGHT);
        }


        if (playerRefactor.GetState() == PlayerRefactor.State.Idle || 0 == playerRefactor.GetMovementX())
        {
            animator.SetBool(WALKING_RIGHT, false);
            animator.SetBool(WALKING_LEFT, false);
            animator.SetBool(DASH_RIGHT, false);
            animator.SetBool(DASH_LEFT, false);
            
            if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame )
                SwitchAnimation(FACING_BACK, FACING_BACK);
                
            if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame )
                SwitchAnimation(FACING_FRONT, FACING_FRONT);
                
            if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame )
                SwitchAnimation(FACING_LEFT, FACING_LEFT);
                
            if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame )
                SwitchAnimation(FACING_RIGHT, FACING_RIGHT); 
        }
        
    }


    private void SwitchAnimation(string newAnim, string newBaseAnim)
    {
        //when animation changes, set current animations to false and set it to the new ones
        //also changes it's base animation to fall back on when an active (walk, dash) animation is done 
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

