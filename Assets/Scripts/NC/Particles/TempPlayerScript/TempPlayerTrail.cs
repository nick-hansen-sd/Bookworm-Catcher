using UnityEngine;

public class TempPlayerTrail : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TrailRenderer trail;

    void Update()
    {
        if(player.GetState() == Player.State.SingleJump || player.GetState() == Player.State.DoubleJump)
        {
            trail.emitting = true;
        }
        else
        {
            trail.emitting = false;
        }
    }
}
