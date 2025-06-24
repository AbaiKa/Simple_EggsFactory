using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EggManager eggManager;
    [SerializeField] private PlatformManager platformManager;

    private void Awake()
    {
        eggManager.Init();
        platformManager.Init();
    }
}
