using UnityEngine;

public class PlayerDust : MonoBehaviour
{
    [SerializeField] private PlayerRefactor player;
    [SerializeField] private ParticleSystem playerDust;
    private ParticleSystem.EmissionModule emission;
    private ParticleSystem.MainModule main;

    private void Awake()
    {
        emission = playerDust.emission;
        main = playerDust.main;
    }

    private void Update()
    {
        if(player.GetState() == PlayerRefactor.State.Moving || player.GetState() == PlayerRefactor.State.Dashing)
        {
            emission.enabled = true;
        }
        else
        {
            emission.enabled = false;
        }

        if(player.GetState() == PlayerRefactor.State.Dashing)
        {
            main.startSize = 1.5f;
        }
        else
        {
            main.startSize = 0.75f;
        }
    }
}
