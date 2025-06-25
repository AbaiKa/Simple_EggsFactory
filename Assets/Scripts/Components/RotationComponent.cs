using UnityEngine;

public class RotationComponent : MonoBehaviour
{
    public enum RotationDirection
    {
        Clockwise,
        CounterClockwise
    }

    [Header("Rotation Settings")]
    public RotationDirection direction = RotationDirection.Clockwise;
    public float speed = 90f;

    void Update()
    {
        float sign = direction == RotationDirection.Clockwise ? -1f : 1f;
        transform.Rotate(0f, 0f, sign * speed * Time.deltaTime);
    }
}