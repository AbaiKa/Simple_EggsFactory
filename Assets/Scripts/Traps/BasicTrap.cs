using UnityEngine;

public class BasicTrap : ATrap
{
    [Header("Base")]
    [SerializeField] private int count;
    protected override void Perform(EggComponent egg)
    {
        if (count != 0)
        {
            var pos = new Vector3(egg.transform.position.x, egg.transform.position.y + 0.25f);
            manager.Spawn(egg.NextID, EggStatus.Sliced, egg.Cost, pos, count);
        }
        egg.DeInit();
    }
}
