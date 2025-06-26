using System;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeManager : MonoBehaviour
{
    private readonly float expGrowth = 1.75f;
    private readonly float priceGrowth = 1.25f;
    private readonly float incomeGrowth = 1.14f;

    public UnityEvent<int> onExpChanged = new UnityEvent<int>();
    private int currentExp;
    public void Init()
    {

    }
    public int GetExpt() => currentExp;
    public void AddExp(int amount)
    {
        currentExp += amount;
        onExpChanged?.Invoke(currentExp);
    }
    public void ResetExp()
    {
        currentExp = 0;
        onExpChanged?.Invoke(currentExp);
    }
    public int GetEggPrice(int price, int level)
    {
        return CalculateValue(price, priceGrowth, level);
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
