using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    PickupAble currentObject = null;
    Transform pickupLocation, heldLocation;
    float throwForce = 250;
    float pickRadius = .75f;
    Rigidbody rBody;
    public Transform PickupLocation
    {
        get
        {
            if (pickupLocation == null)
                pickupLocation = transform.Find("PickUp location");
            return pickupLocation;
        }

        set
        {
            pickupLocation = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        heldLocation = transform.FindDeepChild("HeldObjectOrigin");
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ActionSystem.OnActionDown(ActionSystem.INTERACT))
        {
            if (currentObject)
                Drop(null);
            else
            {
                //TODO: Add layermask
                Collider[] cols = Physics.OverlapSphere(PickupLocation.position, pickRadius);
                foreach (Collider col in cols)
                {
                    if (col.GetComponent<PickupAble>())
                    {
                        Vector3 source = transform.position;
                        source.y = col.transform.position.y;
                        Vector3 dir = source - col.transform.position;
                        RaycastHit hit;
                        if (Physics.Raycast(source, dir, out hit, dir.magnitude))
                        {
                            if (hit.transform == col.transform)
                                PickUp(col.GetComponent<PickupAble>());
                        }
                        else
                        {
                            PickUp(col.GetComponent<PickupAble>());
                        }
                        break;
                    }
                }
            }
        }
    }
    void PickUp(PickupAble p)
    {
        currentObject = p;
        if (currentObject.sp)
            currentObject.sp.OnPickedUp();
        currentObject.Col.enabled = false;
        currentObject.LockTo(heldLocation);

        SpawnManager.Instance.DespawnEvent.Invoke(currentObject.gameObject);
    }
    void Drop(Transform pos)
    {
        currentObject.Col.enabled = true;
        currentObject.Release();
        Vector3 camRight = Camera.main.transform.right * ActionSystem.GetActionAxis(ActionSystem.MOVEAXIS_X);
        camRight.y = 0;
        Vector3 camForward = Camera.main.transform.up * ActionSystem.GetActionAxis(ActionSystem.MOVEAXIS_Y);
        camForward.y = 0;
        Vector3 input = (camRight + camForward);

        if (input.magnitude <= 0.1f)
        {
            input = transform.forward;
        }
        else
            input.Normalize();
        if (pos == null)
        {
            currentObject.RBody.velocity = input * (rBody.velocity.magnitude);
            currentObject.RBody.AddForce(input * throwForce);
        }

        currentObject = null;
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(PickupLocation.position, pickRadius);
    }
}
