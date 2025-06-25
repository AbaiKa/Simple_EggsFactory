using UnityEngine;

public class FillerTrapComponent : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;
    private EggManager manager;
    private void Start()
    {
        manager = FindFirstObjectByType<EggManager>();
    }
    public void OnStrike()
    {
        var collider = Physics2D.OverlapCircle(transform.position, 0.5f, targetMask);

        if (collider != null && collider.TryGetComponent(out EggComponent egg))
        {
            var pos = new Vector3(egg.transform.position.x, egg.transform.position.y + 0.25f);
            manager.Spawn(egg.NextID, EggStatus.Sliced, pos, 1);
        }
    }
}
