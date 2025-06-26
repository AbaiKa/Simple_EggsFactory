using UnityEngine;

public class CollectorComponent : MonoBehaviour
{
    private PlayerManager playerManager;
    public void Init()
    {
        playerManager = FindFirstObjectByType<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EggComponent egg))
        {
            playerManager.AddMoney(egg.Cost);
            egg.DeInit();
        }
    }
}
