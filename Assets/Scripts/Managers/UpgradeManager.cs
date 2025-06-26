using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private readonly float priceGrowth = 1.25f;
    private readonly float incomeGrowth = 1.14f;

    public void Init()
    {

    }

    public int GetEggPrice(int price, int level)
    {
        return CalculateValue(price, priceGrowth, level);
    }
    public int GetEggIncome(int start, int level)
    {
        return CalculateValue(start, incomeGrowth, level);
    }
    public static int CalculateValue(int start, double growth, int level)
    {
        return (int)Math.Round(start * Math.Pow(growth, level));
    }
}
