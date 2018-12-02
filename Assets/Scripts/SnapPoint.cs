using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    public bool IsOccupied;
    public Transform anchorPoint;
    protected PickupAble p;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOccupied)
        {
            Attach(other.GetComponent<PickupAble>());
        }
    }
    public virtual void OnPickedUp()
    {

        if (p)
        {
            p.Release();
            p.sp = null;
            IsOccupied = false;
        }
    }
    protected void Attach(PickupAble obj)
    {
        p = obj;
        IsOccupied = true;

        OnPlace();
    }
    public virtual void OnPlace()
    {
        p.sp = this;
        p.LockTo(anchorPoint);
    }
}
