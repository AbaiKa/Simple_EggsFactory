using UnityEngine;

public class CollectorComponent : MonoBehaviour
{
    private PlayerManager playerManager;
    private UpgradeManager upgradeManager;
    private AudioManager audioManager;
    public void Init()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EggComponent egg))
        {
            audioManager.PlaySound("collect");
            playerManager.AddMoney(egg.Cost);
            upgradeManager.AddExp(egg.Exp);
            egg.DeInit();
        }
    }
}
