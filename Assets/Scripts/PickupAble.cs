using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAble : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rBody;

    [HideInInspector]
    public Collider col;
    // Use this for initialization
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }
}
