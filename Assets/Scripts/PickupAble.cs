using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAble : MonoBehaviour
{
    [HideInInspector]
    private Rigidbody rBody;

    [HideInInspector]
    private Collider col;

    [HideInInspector]
    public SnapPoint sp = null;

    public Rigidbody RBody
    {
        get
        {
            if (rBody == null)
                RBody = GetComponent<Rigidbody>();
            return rBody;
        }

        set
        {
            rBody = value;
        }
    }

    public Collider Col
    {
        get
        {
            if (col == null)
                Col = GetComponent<Collider>();
            return col;
        }

        set
        {
            col = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        RBody = GetComponent<Rigidbody>();
        Col = GetComponent<Collider>();
    }
    public void LockTo(Transform t)
    {
        RBody.isKinematic = true;
        transform.parent = (t);
        transform.position = t.position;
        transform.localEulerAngles = Vector3.zero;

    }
    public void Release()
    {
        transform.parent = (null);
        RBody.isKinematic = false;
        transform.localScale = Vector3.one;
    }

}
