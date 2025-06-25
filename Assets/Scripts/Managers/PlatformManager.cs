using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private string defaultPlatform;
    [SerializeField] private float spawnOffset;
    [Header("Components")]
    [SerializeField] private Transform container;
    [SerializeField] private PlatformComponent[] prefabs;

    private List<PlatformComponent> platforms = new List<PlatformComponent>();
    private Dictionary<string, PlatformComponent> platformsPrefab = new Dictionary<string, PlatformComponent>();

    public void Init()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (!platformsPrefab.ContainsKey(prefabs[i].ID))
            {
                platformsPrefab.Add(prefabs[i].ID, prefabs[i]);
            }
        }

        Spawn(defaultPlatform);
        Spawn("platform_2");
        Spawn("platform_3");
    }

    public void Spawn(string id)
    {
        if (platformsPrefab.ContainsKey(id))
        {
            var p = Instantiate(platformsPrefab[id], container);
            p.transform.localPosition = new Vector3(0, -(platforms.Count * spawnOffset), 0);
            p.Init();
            platforms.Add(p);
            UpdatePlatforms();
        }
    }

    private void UpdatePlatforms()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            platforms[i].SetActiveSorter(platforms.Count - 1 != i);
        }
    }
}
