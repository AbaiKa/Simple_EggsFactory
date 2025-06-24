using UnityEngine;

public class CollectorComponent : MonoBehaviour
{
    public void Init()
    {
        // Initialization
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EggComponent egg))
        {
            Debug.Log($"������� ���� �� {egg.Cost}");
            egg.DeInit();
        }
    }
}
