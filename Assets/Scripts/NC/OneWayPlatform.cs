using System.Collections;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private const string GROUND_LAYER = "GroundLayer";

    private GameObject _currentOneWayPlatform;
    private CapsuleCollider2D _playerCollider;
    private PlayerRefactor _player;

    private void Awake()
    {
        _currentOneWayPlatform = gameObject;
        _player = PlayerRefactor.Instance;
        _playerCollider = _player.gameObject.GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        //if the player is dropping then disable the current collision
        if (_player.IsDropping())
        {
            if (_currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }
    
    private IEnumerator DisableCollision()
    {
        //Debug.Log("Platform_DisableCollision");
        BoxCollider2D platformCollider = _currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(_playerCollider, platformCollider);
        yield return new WaitForSeconds(0.1f);
        Physics2D.IgnoreCollision(_playerCollider, platformCollider, false);
        _player.SetIsDropping(false);
    }
}
