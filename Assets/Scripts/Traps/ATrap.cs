using UnityEngine;

public abstract class ATrap : MonoBehaviour
{
    protected EggManager manager;
    public virtual void Init(EggManager manager)
    {
        this.manager = manager;
    }
    protected abstract void Perform(EggComponent egg);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EggComponent egg))
        {
            if (egg.Status == EggStatus.Whole)
            {
                Perform(egg);
            }
        }
    }
}
