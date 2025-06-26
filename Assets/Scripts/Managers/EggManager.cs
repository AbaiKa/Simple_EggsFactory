using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EggManager : MonoBehaviour
{
    [SerializeField] private EggComponent[] prefabs;
    [SerializeField] private Vector2 defaultSpawnPoint;

    private Dictionary<string, EggComponent> eggs = new Dictionary<string, EggComponent>();

    public void Init()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (!eggs.ContainsKey(prefabs[i].ID))
            {
                eggs.Add(prefabs[i].ID, prefabs[i]);
            }
        }
    }
    public void Spawn(string id, EggStatus status, int cost)
    {
        Spawn(id, status, cost, defaultSpawnPoint);
    }
    public void Spawn(string id, EggStatus status, int cost, int count)
    {
        Spawn(id, status, cost, defaultSpawnPoint, count);
    }
    public void Spawn(string id, EggStatus status, int cost, Vector2 position)
    {
        if (eggs.ContainsKey(id))
        {
            var egg = Spawn(eggs[id], status, cost);
            egg.transform.position = position;
        }
    }
    public void Spawn(string id, EggStatus status, int cost, Vector2 position, int count)
    {
        if (eggs.ContainsKey(id))
        {
            float offset = (eggs[id].transform.localScale.x / 2) + Random.Range(0.1f, 0.3f);
            float startX = position.x - offset * (count - 1) / 2f;

            for (int i = 0; i < count; i++)
            {
                var egg = Spawn(eggs[id], status, cost);
                float x = startX + i * offset;
                egg.transform.position = new Vector3(x, position.y, 0f);
            }
        }
    }
    private EggComponent Spawn(EggComponent prefab, EggStatus status, int cost)
    {
        var egg = Instantiate(prefab);
        egg.Init(status, cost);
        return egg;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(defaultSpawnPoint, 0.5f);
    }
}

[Serializable]
public enum EggStatus
{
    Whole,
    Sliced
}