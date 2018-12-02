using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rBody;
    public float speed = 400;
    public float acceleration = 10;
    Vector3 input;
    public Animator animator;
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
        input = (camRight + camForward);
        if(input.magnitude > 1)
        {
            input.Normalize();
        }
        if (rBody.velocity.magnitude > 0)
        {
            transform.forward = rBody.velocity;
        }
        animator.SetFloat("WalkSpeed", Mathf.Abs(rBody.velocity.magnitude / speed));
    }
    void FixedUpdate()
    {
        //Vector3 targetPos = transform.position;
        //targetPos += (input * speed * Time.deltaTime);
        //rBody.MovePosition(targetPos);
        Vector3 targetSpeed = input * speed;
        Vector3 moveToTargetSpeed = targetSpeed - rBody.velocity;
        if(moveToTargetSpeed.magnitude <= acceleration * Time.deltaTime)
        {
            rBody.AddForce(moveToTargetSpeed, ForceMode.VelocityChange);
        } else
        {
            rBody.AddForce(moveToTargetSpeed.normalized * acceleration, ForceMode.Acceleration);
        }
        

        //rBody.velocity = input * speed * Time.fixedDeltaTime;
    }
}
