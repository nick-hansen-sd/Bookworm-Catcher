using UnityEngine;

public class AC_BookshelfRefactored : MonoBehaviour
{
    public delegate void OnCollisionWormBookshelfFunc(SpriteRenderer spriteRender);
    public static event OnCollisionWormBookshelfFunc OnCollisionWormBookshelf;


    public void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log(collider.gameObject.tag);
        //checks if the object collision is a boookworm
        if (collider.gameObject.CompareTag("Bookworm"))
        {
            //grabs renderer of prefab instance and invokes function
            SpriteRenderer spriteRender = gameObject.GetComponentInChildren<SpriteRenderer>();
            OnCollisionWormBookshelf?.Invoke(spriteRender);
            // Debug.Log("hit");
            // Debug.Log(gameObject.name);
        }
            
    }
}
