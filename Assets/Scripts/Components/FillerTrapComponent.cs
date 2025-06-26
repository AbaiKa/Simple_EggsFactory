using UnityEngine;

public class FillerTrapComponent : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float multiplier;
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
            int cost = (int)(egg.Cost * multiplier);
            int exp = (int)(egg.Exp * multiplier);
            manager.Spawn(egg.NextID, EggStatus.Sliced, cost, exp, pos, 1);
        }
    }
}
