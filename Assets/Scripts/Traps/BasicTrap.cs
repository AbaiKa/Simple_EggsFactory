using UnityEngine;

public class BasicTrap : ATrap
{
    [Header("Base")]
    [SerializeField] private int count;
    protected override void Perform(EggComponent egg)
    {
        var pos = new Vector3(egg.transform.position.x, egg.transform.position.y + 0.25f);
        manager.Spawn(egg.NextID, EggStatus.Sliced, pos, count);
        egg.DeInit();
    }
}
