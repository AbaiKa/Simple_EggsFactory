using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EggManager eggManager;
    [SerializeField] private PlatformManager platformManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private UIManager uiManager;
    private void Awake()
    {
        eggManager.Init();
        platformManager.Init();
        upgradeManager.Init();
        playerManager.Init();
        audioManager.Init();
        uiManager.Init();

        playerManager.AddMoney(100);
    }
}
