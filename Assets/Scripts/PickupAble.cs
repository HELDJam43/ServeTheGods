using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAble : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rBody;
    // Use this for initialization
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }
}
