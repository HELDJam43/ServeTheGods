using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    public Food DesiredFood;
    public GameObject OrderBubble;

    // Use this for initialization
    public God()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains(DesiredFood.name.ToLower()))
        {
            //TODO MORE here instead of just getting rid of bubble
            Debug.Log("NUM NUM");
            Destroy(OrderBubble);
            LevelManager.GodOrderDelivered();
            Destroy(collision.gameObject);
        }
    }

    public void SetDesiredFood(Food desiredFood, GameObject orderBubble)
    {
        DesiredFood = desiredFood;
        OrderBubble = orderBubble;
    }
}
