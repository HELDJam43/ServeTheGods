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
    float inputAngle = 0;
    static float ROTATIONSPEED = 4;
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
        
        //animator.SetFloat("WalkSpeed", Mathf.Abs(rBody.velocity.magnitude / speed));
        animator.SetFloat("WalkSpeed", Mathf.Abs(rBody.velocity.magnitude));
    }
    void FixedUpdate()
    {
        if (input.magnitude > 0)
        {
            float desiredAngle = Mathf.Atan2(input.z, input.x);
            desiredAngle = (desiredAngle + Mathf.PI * 2) % (Mathf.PI * 2);
            float angleDelta = -Mathf.DeltaAngle(desiredAngle, inputAngle);
            if(Mathf.Abs(angleDelta) > Mathf.PI * .7f || Mathf.Abs(angleDelta) < Mathf.PI * ROTATIONSPEED * Time.fixedDeltaTime)
            {
                inputAngle = desiredAngle;
            } else
            {
                inputAngle = inputAngle + Mathf.Sign(angleDelta) * ROTATIONSPEED * Mathf.PI * Time.fixedDeltaTime;
            }

            input = new Vector3(Mathf.Cos(inputAngle), 0, Mathf.Sin(inputAngle));
            transform.forward = input;
        }

        
        /*Vector3 targetSpeed = input * speed;
        Vector3 moveToTargetSpeed = targetSpeed - rBody.velocity;
        if(moveToTargetSpeed.magnitude <= acceleration * Time.deltaTime)
        {
            rBody.AddForce(moveToTargetSpeed, ForceMode.VelocityChange);
        } else
        {
            rBody.AddForce(moveToTargetSpeed.normalized * acceleration, ForceMode.Acceleration);
        }*/

        rBody.velocity = input * speed * Time.fixedDeltaTime;
    }
}
