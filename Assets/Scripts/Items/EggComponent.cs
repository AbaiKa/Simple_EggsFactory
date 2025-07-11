using UnityEngine;

public class EggComponent : MonoBehaviour
{
    [field: Header("Properties")]
    [field: SerializeField] public string ID { get; private set; }
    [field: SerializeField] public string NextID { get; private set; }
    public int Cost { get; private set; }
    public int Exp { get; private set; }
    [Header("Components")]
    [SerializeField] private Rigidbody2D rigidbodyComponent;

    public EggStatus Status { get; private set; }
    public void Init(EggStatus status, int cost, int exp)
    {
        Cost = cost;
        Exp = exp;
        SetStatus(status);
    }
    public void DeInit()
    {
        Destroy(gameObject);
    }
    public void SetStatus(EggStatus status)
    {
        Status = status;
    }
    public void SetActiveGravity(bool value)
    {
        rigidbodyComponent.gravityScale = value ? 1 : 0;
    }
}
