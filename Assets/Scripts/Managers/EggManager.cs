using System;
using System.Collections;
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

        StartCoroutine(SpawnRoutine());
    }
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Spawn("basic", EggStatus.Whole);
            yield return new WaitForSeconds(Random.Range(2, 4));
        }
    }
    public void Spawn(string id, EggStatus status)
    {
        Spawn(id, status, defaultSpawnPoint);
    }
    public void Spawn(string id, EggStatus status, int count)
    {
        Spawn(id, status, defaultSpawnPoint, count);
    }
    public void Spawn(string id, EggStatus status, Vector2 position)
    {
        if (eggs.ContainsKey(id))
        {
            var egg = Spawn(eggs[id], status);
            egg.transform.position = position;
        }
    }
    public void Spawn(string id, EggStatus status, Vector2 position, int count)
    {
        if (eggs.ContainsKey(id))
        {
            float offset = (eggs[id].transform.localScale.x / 2) + Random.Range(0.1f, 0.3f);
            float startX = position.x - offset * (count - 1) / 2f;

            for (int i = 0; i < count; i++)
            {
                var egg = Spawn(eggs[id], status);
                float x = startX + i * offset;
                egg.transform.position = new Vector3(x, position.y, 0f);
            }
        }
    }
    private EggComponent Spawn(EggComponent prefab, EggStatus status)
    {
        var egg = Instantiate(prefab);
        egg.Init(status);
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