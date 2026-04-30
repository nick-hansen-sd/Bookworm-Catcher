using System;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    [SerializeField] private PlayerRefactor player;
    [SerializeField] private TrailRenderer trail;

    void Update()
    {
        if(player.GetState() == PlayerRefactor.State.SingleJump || player.GetState() == PlayerRefactor.State.DoubleJump)
        {
            trail.emitting = true;
        }
        else
        {
            trail.emitting = false;
        }
    }
}
