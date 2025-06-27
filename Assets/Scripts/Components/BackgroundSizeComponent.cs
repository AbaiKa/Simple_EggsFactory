using UnityEngine;

public class BackgroundSizeComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteComponent;
    private void Start()
    {
        Vector2 spriteSize = spriteComponent.sprite.bounds.size;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * Camera.main.aspect;

        Vector3 scale = transform.localScale;
        scale.x = worldScreenWidth / spriteSize.x;
        scale.y = scale.x;
        transform.localScale = scale;
    }
}
