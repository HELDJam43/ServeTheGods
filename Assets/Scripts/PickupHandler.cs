using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    PickupAble currentObject = null;
    Transform pickupLocation, heldLocation;
    float throwForce = 150;
    float pickRadius = .75f;
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
    IEnumerator Start()
    {
        heldLocation = transform.Find("Held location");
        yield return null;
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
        currentObject.rBody.isKinematic = true;
        currentObject.transform.parent = (transform);
        currentObject.transform.position = heldLocation.position;
        currentObject.transform.localEulerAngles = Vector3.zero;
    }
    void Drop(Transform pos)
    {
        currentObject.transform.parent = (null);
        currentObject.rBody.isKinematic = false;
        if (pos == null)
        {

            currentObject.rBody.AddForce(transform.forward * throwForce);
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
