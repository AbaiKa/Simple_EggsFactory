using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private EggButton[] eggButtons;

    public void Init()
    {
        var egg = FindFirstObjectByType<EggManager>();
        var player = FindFirstObjectByType<PlayerManager>();
        var upgrade = FindFirstObjectByType<UpgradeManager>();

        for(int i = 0; i < eggButtons.Length; i++)
        {
            eggButtons[i].Init(egg, upgrade, player);
            eggButtons[i].SetActive(i == 0);
            eggButtons[i].onBuy.AddListener(UpdateButtons);
        }

        player.onMoneyChanged.AddListener(UpdateCoins);
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
}