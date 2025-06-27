using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button showButton;
    [SerializeField] private Button hideButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private UpgradeButton defaultUpgrade;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI upgradeValueText;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI buyText;
    [SerializeField] private UpgradeButton[] upgradeButtons;

    private PlayerManager playerManager;
    private UpgradeManager upgradeManager;
    private PlatformManager platformManager;
    private AudioManager audioManager;

    private int currentPrice;
    private UpgradeButton currentUpgrade;
    public void Init(PlayerManager player, UpgradeManager upgrade)
    {
        playerManager = player;
        upgradeManager = upgrade;
        platformManager = FindFirstObjectByType<PlatformManager>();
        audioManager = FindFirstObjectByType<AudioManager>();

        showButton.onClick.AddListener(Show);
        hideButton.onClick.AddListener(Hide);
        buyButton.onClick.AddListener(Upgrade);

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].Init();
            upgradeButtons[i].onSelect.AddListener(OnSelectUpgrade);
        }

        Hide();
    }
    public UpgradeButton GetUpgradeButton(string id)
    {
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if (upgradeButtons[i].ID.Equals(id))
            {
                return upgradeButtons[i];
            }
        }
        return null;
    }
    private void OnSelectUpgrade(UpgradeButton upgrade)
    {
        currentUpgrade = upgrade;
        UpdateDescription();
        audioManager.PlaySound("click");
    }
    private void UpdateDescription()
    {
        descriptionText.text = currentUpgrade.Description;
        int level = playerManager.GetUpgradeLevel(currentUpgrade.ID);
        int upgrade = level * currentUpgrade.Value;
        upgradeValueText.text = $"Current bonus <color=green>{upgrade}</color>";
        string buy = "Max";
        if (currentUpgrade.MaxLevel > level)
        {
            currentPrice = upgradeManager.GetUpgradePrice(currentUpgrade.Price, level);
            buy = $"Buy <color=green>{currentPrice}</color>";
        }
        buyText.text = buy;

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int l = playerManager.GetUpgradeLevel(upgradeButtons[i].ID);
            string text = "max";
            if (l < upgradeButtons[i].MaxLevel)
            {
                int price = upgradeManager.GetUpgradePrice(upgradeButtons[i].Price, l);
                text = $"Lvl. {l + 1} <color=#00D3FF>{price}</color>";
            }
            upgradeButtons[i].UpdateItem(text);
        }
    }
    private void Show()
    {
        panel.SetActive(true);
        currentUpgrade = defaultUpgrade;
        levelText.text = $"Level {playerManager.GetUpgradeLevel("upgrade") + 1}";
        expText.text = $"Points {playerManager.GetExp()}";
        UpdateDescription();
        audioManager.PlaySound("click");
    }
    private void Hide()
    {
        panel.SetActive(false);
        audioManager.PlaySound("click");
    }
    private void Upgrade()
    {
        int l = playerManager.GetUpgradeLevel(currentUpgrade.ID);
        if (l >= currentUpgrade.MaxLevel)
        {
            return;
        }
        if (playerManager.SpendExp(currentPrice))
        {
            playerManager.Upgrade(currentUpgrade.ID);
            SwitchUpgrade(currentUpgrade.ID);
            UpdateDescription();
            expText.text = $"Points {playerManager.GetExp()}";
        }
        audioManager.PlaySound("click");
    }

    private void SwitchUpgrade(string id)
    {
        if (id.Equals("platform"))
        {
            platformManager.SpawnNext();
        }
    }
}
