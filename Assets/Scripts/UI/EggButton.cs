using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EggButton : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private string id;
    [SerializeField] private int income;
    [SerializeField] private int exp;
    [SerializeField] private int price;
    [SerializeField] private float eggPerSecond;
    [SerializeField] private Color color;
    [Header("Components")]
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buttonComponent;
    [SerializeField] private GameObject enabledPanel;
    [SerializeField] private GameObject hiddenPanel;
    
    public UnityEvent onBuy = new UnityEvent();

    public bool IsEnabled { get; private set; }

    private int currentLevel;
    private float interval;
    private float elapsedTime;

    private EggManager egg;
    private UpgradeManager upgrade;
    private PlayerManager player;
    public void Init(EggManager egg, UpgradeManager upgrade, PlayerManager player)
    {
        IsEnabled = false;
        interval = 1 / eggPerSecond;
        this.egg = egg;
        this.upgrade = upgrade;
        this.player = player;

        buttonComponent.onClick.AddListener(OnClick);
        player.onMoneyChanged.AddListener(UpdatePrice);

        currentLevel = player.GetUpgradeLevel(id);
        progressBar.fillAmount = 1;
    }

    private void Update()
    {
        if (currentLevel == 0)
            return;

        elapsedTime += Time.deltaTime;
        if (elapsedTime > interval)
        {
            elapsedTime = upgrade.IsEggSpeed() ? interval / 2f : 0;
            egg.Spawn(id, EggStatus.Whole, income, exp);
            if (upgrade.IsDoubleEggs())
            {
                egg.Spawn(id, EggStatus.Whole, income, exp);
            }
        }

        progressBar.fillAmount = elapsedTime / interval;
    }
    public void SetActive(bool value)
    {
        enabledPanel.SetActive(value);
        hiddenPanel.SetActive(!value);
        UpdateValues();
    }
    private void UpdatePrice(int money)
    {
        currentLevel = player.GetUpgradeLevel(id);
        int currentPrice = upgrade.GetEggPrice(price, currentLevel);
        priceText.color = money >= currentPrice ? Color.white : Color.red;
        priceText.text = NumberFormatter.Format(currentPrice);
        var color = money >= currentPrice ? Color.white : Color.red;
    }
    private void UpdateValues()
    {
        currentLevel = player.GetUpgradeLevel(id);
        int currentIncome = upgrade.GetEggIncome(income, currentLevel);
        incomeText.text = NumberFormatter.Format(currentIncome);
    }
    private void OnClick()
    {
        currentLevel = player.GetUpgradeLevel(id);
        int currentPrice = upgrade.GetEggPrice(price, currentLevel);

        if (player.SpendMoney(currentPrice))
        {
            IsEnabled = true;
            onBuy?.Invoke();
            player.Upgrade(id);
            UpdateValues();
            UpdatePrice(player.GetMoney());
            if (upgrade.IsDoubleUpgrade())
            {
                onBuy?.Invoke();
                player.Upgrade(id);
                UpdateValues();
                UpdatePrice(player.GetMoney());
            }
        }
    }
}
