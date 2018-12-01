using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rBody;
    float speed = 400;
    Vector3 input;
    // Use this for initialization
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        Vector3 camRight = Camera.main.transform.right * ActionSystem.GetActionAxis(ActionSystem.MOVEAXIS_X);
        camRight.y = 0;
        Vector3 camForward = Camera.main.transform.up * ActionSystem.GetActionAxis(ActionSystem.MOVEAXIS_Y);
        camForward.y = 0;
        input = camRight.normalized + camForward.normalized;
        if (input.magnitude > 0)
            transform.forward = input;
    }
    void FixedUpdate()
    {
        //Vector3 targetPos = transform.position;
        //targetPos += (input * speed * Time.deltaTime);
        //rBody.MovePosition(targetPos);
        rBody.velocity = input * speed * Time.fixedDeltaTime;
    }
}
