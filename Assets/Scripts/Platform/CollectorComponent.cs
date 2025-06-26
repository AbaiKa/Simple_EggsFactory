using UnityEngine;

public class CollectorComponent : MonoBehaviour
{
    private PlayerManager playerManager;
    private UpgradeManager upgradeManager;
    public void Init()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EggComponent egg))
        {
            playerManager.AddMoney(egg.Cost);
            upgradeManager.AddExp(egg.Exp);
            egg.DeInit();
        }
    }
}
