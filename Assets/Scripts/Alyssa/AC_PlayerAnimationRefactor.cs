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
                    ActionAnimation(DASH_LEFT, FACING_LEFT);
                
                else if (0 < playerRefactor.GetMovementX())
                    ActionAnimation(DASH_RIGHT, FACING_RIGHT);
            }
            
        }
        
        
        if(playerRefactor.GetState() == PlayerRefactor.State.Moving)
        {    
            if (0 > playerRefactor.GetMovementX())
                ActionAnimation(WALKING_LEFT, FACING_LEFT);
            
            else if (0 < playerRefactor.GetMovementX())
                ActionAnimation(WALKING_RIGHT, FACING_RIGHT);
        }


        if (playerRefactor.GetState() == PlayerRefactor.State.Idle || 0 == playerRefactor.GetMovementX())
        {
            animator.SetBool(WALKING_RIGHT, false);
            animator.SetBool(WALKING_LEFT, false);
            animator.SetBool(DASH_RIGHT, false);
            animator.SetBool(DASH_LEFT, false);
            
            if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame )
                SwitchBaseAnimation(FACING_BACK);
                
            if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame )
                SwitchBaseAnimation(FACING_FRONT);
                
            if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame )
                SwitchBaseAnimation(FACING_LEFT);
                
            if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame )
                SwitchBaseAnimation(FACING_RIGHT); 
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

