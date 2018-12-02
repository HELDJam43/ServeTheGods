using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.transform.position;
        target.x = 0;
        //target.z = Mathf.Clamp(target.z, -5, 5)/5;
        target.z /= 6;
        transform.LookAt(target);
    }
}
