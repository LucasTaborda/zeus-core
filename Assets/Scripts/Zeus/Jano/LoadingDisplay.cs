using UnityEngine;

public abstract class LoadingDisplay : MonoBehaviour
{
    public virtual void Refresh(float porcentage) { }

    public virtual void Show() { }

    public virtual void Hide() { }
}
