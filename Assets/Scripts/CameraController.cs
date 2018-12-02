using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    float startY, offsetY;
    // Use this for initialization
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = transform.position;

        float x = player.transform.position.x;
        x = Mathf.Clamp(x, -7, 2);
        //x /= 2;
        target.y = startY + x;
        transform.position = target;

        target = player.transform.position;
        target.x = 0;
        //target.z = Mathf.Clamp(target.z, -5, 5)/5;
        target.z /= 6;
        transform.LookAt(target);

        

    }
}
