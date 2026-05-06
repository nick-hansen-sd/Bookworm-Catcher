using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    [SerializeField] private Vector2 parallaxFactor;
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    
    private float spriteWidthInUnits;

    private const int REPEAT_BACKGROUND_TIMES = 8;
    
    
    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        spriteRenderer.size = new Vector2(spriteRenderer.size.x * REPEAT_BACKGROUND_TIMES, spriteRenderer.size.y);
        
        float textureWidth = spriteRenderer.sprite.texture.width;
        float pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;
        
        spriteWidthInUnits = (textureWidth * 15) / pixelsPerUnit;
    }
    
    private void LateUpdate()
    {
        Vector3 currentCameraPosition = cameraTransform.position;
        Vector3 cameraMovementThisFrame = currentCameraPosition - previousCameraPosition;
        transform.position -= new Vector3(cameraMovementThisFrame.x * parallaxFactor.x, 0f, 0f);
        
        previousCameraPosition = currentCameraPosition;

        float deltaX = cameraTransform.position.x - transform.position.x;

        if (deltaX >= spriteWidthInUnits)
        {
            transform.position += new Vector3(spriteWidthInUnits, 0f, 0f);
        }
        else if (deltaX <= -spriteWidthInUnits)
        {
            transform.position -= new Vector3(spriteWidthInUnits, 0f, 0f);
        }
    }
}