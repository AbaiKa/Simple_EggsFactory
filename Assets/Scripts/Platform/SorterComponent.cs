using UnityEngine;

public class SorterComponent : MonoBehaviour
{
    [SerializeField] private EggStatus interactStatus;
    [SerializeField] private EggStatus targetStatus;
    [SerializeField] private bool enableGravity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EggComponent egg))
        {
            if (egg.Status == interactStatus)
            {
                egg.SetStatus(targetStatus);
                egg.SetActiveGravity(enableGravity);
            }
        }
    }
}
