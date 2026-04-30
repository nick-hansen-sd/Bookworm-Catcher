using UnityEngine;

public class PlayerPaper : MonoBehaviour
{
    [SerializeField] private PlayerRefactor player;
    [SerializeField] private ParticleSystem playerPaper;

    private ParticleSystem.EmissionModule emission;

    private void Awake()
    {
        emission = playerPaper.emission;
    }

    private void Update()
    {
        if(player.GetState() == PlayerRefactor.State.SingleJump || player.GetState() == PlayerRefactor.State.DoubleJump)
        {
            emission.enabled = true;
        }
        else
        {
            emission.enabled = false;
        }
    }
}
