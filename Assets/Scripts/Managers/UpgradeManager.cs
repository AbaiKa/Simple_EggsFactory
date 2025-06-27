using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{
    private readonly float expGrowth = 1.75f;
    private readonly float priceGrowth = 1.25f;
    private readonly float incomeGrowth = 1.14f;

    public UnityEvent<int> onExpChanged = new UnityEvent<int>();
    private int currentExp;

    private PlayerManager playerManager;
    private UpgradePanel upgradePanel;
    private UpgradeManager upgradeManager;
    public void Init()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
        upgradePanel = FindFirstObjectByType<UpgradePanel>();
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
    }
    public int GetExpt() => currentExp;
    public void AddExp(int amount)
    {
        int targetAmount = upgradeManager.IsDoubleExp() ? amount * 2 : amount;
        currentExp += targetAmount;
        onExpChanged?.Invoke(currentExp);
    }
    public void ResetExp()
    {
        currentExp = 0;
        onExpChanged?.Invoke(currentExp);
    }
    public int EvaluateMoney(int money)
    {
        int level = playerManager.GetUpgradeLevel("coin_add");
        int targetExp = money;
        if (level != 0)
        {
            var upgrade = upgradePanel.GetUpgradeButton("coin_add");
            float percent = (upgrade.Value * level) / 100;
            targetExp = (int)(money * (percent + 1));
        }
        if (IsDoubleCoins())
        {
            targetExp *= 2;
        }
        return targetExp;
    }
    public int EvaluateExp(int exp)
    {
        int level = playerManager.GetUpgradeLevel("exp_add");
        int targetExp = exp;
        if (level != 0)
        {
            var upgrade = upgradePanel.GetUpgradeButton("expp_add");
            float percent = (upgrade.Value * level) / 100;
            targetExp = (int)(exp * (percent + 1));
        }

        return targetExp;
    }
    public int GetEggPrice(int price, int level)
    {
        int targetPrice = CalculateValue(price, priceGrowth, level);
        int upgradeLevel = playerManager.GetUpgradeLevel("cost_minus");

        if (upgradeLevel != 0)
        {
            var upgrade = upgradePanel.GetUpgradeButton("cost_minus");
            float discount = (upgrade.Value * upgradeLevel) / 100f;
            targetPrice = (int)(targetPrice * (1f - discount));
        }

        return targetPrice;
    }
    public bool IsDoubleUpgrade()
    {
        int level = playerManager.GetUpgradeLevel("double_eggs_buy");
        if (level != 0)
        {
            var upgrade = upgradePanel.GetUpgradeButton("double_eggs_buy");
            int percent = upgrade.Value * level;
            return Random.Range(0, 100) < percent;
        }
        return false;
    }
    public bool IsEggSpeed()
    {
        int level = playerManager.GetUpgradeLevel("platform_something");
        if (level != 0)
        {
            var upgrade = upgradePanel.GetUpgradeButton("platform_something");
            int percent = upgrade.Value * level;
            return Random.Range(0, 100) < percent;
        }
        return false;
    }
    public bool IsDoubleCoins()
    {
        int level = playerManager.GetUpgradeLevel("double_coins");
        if (level != 0)
        {
            var upgrade = upgradePanel.GetUpgradeButton("double_coins");
            int percent = upgrade.Value * level;
            return Random.Range(0, 100) < percent;
        }

        return false;
    }
    public bool IsDoubleEggs()
    {
        int level = playerManager.GetUpgradeLevel("double_eggs_spawn");
        if (level != 0)
        {
            var upgrade = upgradePanel.GetUpgradeButton("double_eggs_spawn");
            int percent = upgrade.Value * level;
            return Random.Range(0, 100) < percent;
        }

        return false;
    }
    public bool IsDoubleExp()
    {
        int level = playerManager.GetUpgradeLevel("platform_bonus");
        if (level != 0)
        {
            var upgrade = upgradePanel.GetUpgradeButton("platform_bonus");
            int percent = upgrade.Value * level;
            return Random.Range(0, 100) < percent;
        }

        return false;
    }
    public int GetEggIncome(int start, int level)
    {
        return CalculateValue(start, incomeGrowth, level);
    }
    public int GetExpLevelValue(int currentLevel)
    {
        return CalculateValue(20, expGrowth, currentLevel);
    }
    public int GetUpgradePrice(int start, int level)
    {
        return CalculateValue(start, priceGrowth, level);
    }
    public static int CalculateValue(int start, double growth, int level)
    {
        return (int)Math.Round(start * Math.Pow(growth, level));
    }
}
