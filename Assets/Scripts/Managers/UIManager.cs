using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private UpgradePanel upgradePanel;
    [SerializeField] private SettingsPanel settingsPanel;
    [SerializeField] private MainMenuPanel mainMenuPanel;
    [SerializeField] private EggButton[] eggButtons;
    [SerializeField, Range(0, 1)] private float defaultMusic;
    [SerializeField, Range(0, 1)] private float defaultSound;

    private EggManager eggManager;
    private PlayerManager playerManager;
    private UpgradeManager upgradeManager;
    public void Init()
    {
        eggManager = FindFirstObjectByType<EggManager>();
        playerManager = FindFirstObjectByType<PlayerManager>();
        upgradeManager = FindFirstObjectByType<UpgradeManager>();

        for(int i = 0; i < eggButtons.Length; i++)
        {
            eggButtons[i].Init(eggManager, upgradeManager, playerManager);
            eggButtons[i].SetActive(i == 0);
            eggButtons[i].onBuy.AddListener(UpdateButtons);
        }

        upgradePanel.Init(playerManager, upgradeManager);
        settingsPanel.Init(defaultMusic, defaultSound);
        mainMenuPanel.Init();

        playerManager.onMoneyChanged.AddListener(UpdateCoins);
        upgradeManager.onExpChanged.AddListener(UpdateExp);
        UpdateExp(upgradeManager.GetExpt());
    }

    private void UpdateButtons()
    {
        for (int i = 1; i < eggButtons.Length; i++)
        {
            bool shouldActivate = eggButtons[i - 1].IsEnabled;
            eggButtons[i].SetActive(shouldActivate);
        }
    }
    private void UpdateCoins(int money)
    {
        coinsText.text = NumberFormatter.Format(money);
    }

    private void UpdateExp(int exp)
    {
        int level = playerManager.GetUpgradeLevel("upgrade");
        int target = upgradeManager.GetExpLevelValue(level);
        expText.text = $"exp {exp}/{target}";
        if (exp >= target)
        {
            playerManager.Upgrade("upgrade");
            playerManager.AddExp(Random.Range(3 * level + 1, 6 * level + 1));
            upgradeManager.ResetExp();
        }
    }
}