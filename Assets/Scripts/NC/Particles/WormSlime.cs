using UnityEngine;

public class WormSlime : MonoBehaviour
{
    [SerializeField] private WormPatrol bookworm;
    [SerializeField] private ParticleSystem slime;
    
    private bool detachedSlime = false;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Worm State: " + bookworm.GetState());
        if (!detachedSlime && bookworm != null && bookworm.GetState() == WormPatrol.StateMachine.Caught)
        {
            detachedSlime = true;
            //remove slime from parent
            slime.transform.SetParent(null);
            //continue existing particles, stop emitting new ones
            slime.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            //destroy after duration + lifetime
            Destroy(slime.gameObject, slime.main.duration + slime.main.startLifetime.constantMax);
        }
    }
}
