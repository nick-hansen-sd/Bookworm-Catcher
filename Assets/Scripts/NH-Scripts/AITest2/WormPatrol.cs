using System;
using UnityEngine;

public class WormPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private float obstacleRaycastDistance = 2f;

    [SerializeField] private bool movingRight = true; // This is a serialize field so the starting direction can be changed from the editor.

    [SerializeField] private Transform groundDetection;

    [SerializeField] private LayerMask groundLayerMask;

    public enum StateMachine
    {
        Patrol,
        Caught
    }

    public StateMachine currentState;

    private void Start()
    {
        currentState = StateMachine.Patrol;

        // Start worm facing left if movingRight is disabled in the editor when the game starts
        if (!movingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case StateMachine.Patrol:
                Patrol();
                break;
            case StateMachine.Caught:
                // Do nothing
                break;
        }
    }

    private void Patrol()
    {
        // Moves worm
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Checks for when ground ends
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, obstacleRaycastDistance, groundLayerMask);
        RaycastHit2D pathAheadInfo = Physics2D.Raycast(groundDetection.position, Vector2.right, obstacleRaycastDistance, groundLayerMask);

        // Changes direction when ground ends
        if (groundInfo.collider == false)
        {
            ChangeDirectionLeftRight();
        } else if (pathAheadInfo.collider != false)
        {
            ChangeDirectionLeftRight();
        }
    }

    public void BookwormCaught()
    {
        currentState = StateMachine.Caught;
    }

    private void ChangeDirectionLeftRight()
    {
        if (movingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        } else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.collider.CompareTag("Ladder"))
        {
            // Ignore collisions with ladders
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
        }

        if (collision.collider.CompareTag("Slime"))
        {
            // Ignore collisions with slime
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
        }
        
        if (collision.collider.CompareTag("Bookworm"))
        {
            // Ignore collisions with other worms
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider, true);
        }
        
    }

    //------Moises-------
    public StateMachine GetState()
    {
        return currentState;
    }
    //--------------------
}
