using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public UnityEvent<int> onExpChanged = new UnityEvent<int>();
    public UnityEvent<int> onMoneyChanged = new UnityEvent<int>();
    public UnityEvent<string, int> onUpgrade = new UnityEvent<string, int>();

    private int money;
    private int exp;
    private Dictionary<string, int> upgrades = new Dictionary<string, int>();
    public void Init()
    {
        exp = 0;
        money = 0;
    }
    public int GetExp() => exp;
    public void AddExp(int amount)
    {
        exp += amount;
        onExpChanged?.Invoke(exp);
    }
    public bool SpendExp(int amount)
    {
        if (exp >= amount)
        {
            exp -= amount;
            onExpChanged?.Invoke(exp);
            return true;
        }
        return false;
    }
    public int GetMoney() => money;
    public void AddMoney(int amount)
    {
        money += amount;
        onMoneyChanged?.Invoke(money);
    }
    public bool SpendMoney(int price)
    {
        if (money >= price)
        {
            money -= price;
            onMoneyChanged?.Invoke(money);
            return true;
        }

        return false;
    }

    public void Upgrade(string id)
    {
        if (!upgrades.ContainsKey(id))
        {
            upgrades.Add(id, 1);
        }
        else
        {
            upgrades[id]++;
        }

        onUpgrade?.Invoke(id, upgrades[id]);
    }

    public int GetUpgradeLevel(string id)
    {
        if (upgrades.ContainsKey(id))
        {
            return upgrades[id];
        }
        return 0;
    }
}
