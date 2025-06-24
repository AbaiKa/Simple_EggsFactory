using UnityEngine;

public class PlatformComponent : MonoBehaviour
{
    [field: Header("Properties")]
    [field: SerializeField] public string ID { get; private set; }
    [Header("Components")]
    [SerializeField] private CollectorComponent collector;
    [SerializeField] private GameObject sorterPlatform;
    [SerializeField] private GameObject noSorterPlatform;
    [SerializeField] private ATrap[] traps;

    public void Init()
    {
        collector.Init();

        var eggManager = FindFirstObjectByType<EggManager>();
        for (int i = 0; i < traps.Length; i++)
        {
            traps[i].Init(eggManager);
        }
    }

    public void SetActiveSorter(bool value)
    {
        sorterPlatform.SetActive(value);
        noSorterPlatform.SetActive(!value);
    }
}
