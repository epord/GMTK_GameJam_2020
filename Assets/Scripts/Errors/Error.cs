using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Error : MonoBehaviour
{
    [HideInInspector]
    public bool isActive = false;

    public void Toggle()
    {
        if (isActive) Deactivate();
        else Activate();
    }

    public void Activate()
    {
        if (!isActive)
        {
            isActive = true;
            OnActivate();
        }
    }
    public void Deactivate()
    {
        if (isActive)
        {
            isActive = false;
            OnDeactivate();
        }
    }

    public virtual void OnActivate() { }
    public virtual void OnDeactivate() { }
    public virtual void Apply() { }

    void Update()
    {
        if (isActive)
        {
            Apply();
        }
    }
}
