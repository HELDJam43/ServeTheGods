using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    PickupAble currentObject = null;
    Transform pickupLocation, heldLocation;
    float throwForce = 150;
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
        currentObject.rBody.isKinematic = true;
        currentObject.col.enabled = false;
        currentObject.transform.parent = (heldLocation);
        currentObject.transform.position = heldLocation.position;
        currentObject.transform.localEulerAngles = Vector3.zero;

        SpawnManager.Instance.DespawnEvent.Invoke(currentObject.gameObject);
    }
    void Drop(Transform pos)
    {
        currentObject.transform.parent = (null);
        currentObject.rBody.isKinematic = false;
        currentObject.col.enabled = true;
        if (pos == null)
        {
            currentObject.rBody.velocity = rBody.velocity / 1.5f;
            currentObject.rBody.AddForce(transform.forward * throwForce);
        }
        currentObject.transform.localScale = Vector3.one;
        currentObject = null;
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(PickupLocation.position, pickRadius);
    }
}
