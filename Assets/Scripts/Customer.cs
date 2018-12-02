using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {

    public CustomerBehavior Behavior { get { return _behavior; } set { _behavior = value; } }
    public Food DesiredFood;
    public GameObject OrderBubble;

    // Use this for initialization
    public Customer() {
        
	}

    // Update is called once per frame
    void Update () {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains(DesiredFood.name.ToLower()))
        {
            //TODO MORE here instead of just getting rid of bubble
            Destroy(OrderBubble);
            LevelManager.CustomerOrderDelivered();
            Destroy(collision.gameObject);
        }
    }

    private CustomerBehavior _behavior;

    public void AssignRandomBehavior()
    {
        CustomerBehavior.Attitude attitude = TempRanBehavior();
        switch (attitude)
        {
            case CustomerBehavior.Attitude.Neutral:
                {
                    _behavior = new NeutralCustomer();
                    break;
                }
            case CustomerBehavior.Attitude.Happy:
                {
                    _behavior = new HappyCustomer();
                    break;
                }
            case CustomerBehavior.Attitude.Mild:
                {
                    _behavior = new MildCustomer();
                    break;
                }
            case CustomerBehavior.Attitude.Angry:
                {
                    _behavior = new AngryCustomer();
                    break;
                }
        }
    }

    public void SetDesiredFood(Food desiredFood,GameObject orderBubble)
    {
        DesiredFood = desiredFood;
        OrderBubble = orderBubble;
    }

    private CustomerBehavior.Attitude TempRanBehavior()
    {
        Array values = Enum.GetValues(typeof(CustomerBehavior.Attitude));
        int x = UnityEngine.Random.Range(0, values.Length);
        return (CustomerBehavior.Attitude)values.GetValue(x);
    }
}
