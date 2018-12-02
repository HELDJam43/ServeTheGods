using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {

    public CustomerBehavior Behavior { get { return _behavior; } set { _behavior = value; } }
    public Food DesiredFood;
    public GameObject OrderBubble;

    private bool wander = false;
    private float Speed = 0.5f;
    private Vector3 wayPoint;
    private float Range = 5;


    // Update is called once per frame
    void Update () {

        //// this is called every frame
        //// do move code here

        if (wander && transform.position.y < 1) 
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Speed * Time.deltaTime;
            if ((transform.position - wayPoint).magnitude < 3)
            {
                // when the distance between us and the target is less than 3
                // create a new way point target
                Wander();
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains(DesiredFood.name.ToLower()))
        {
            //TODO MORE here instead of just getting rid of bubble
            Destroy(OrderBubble);
            LevelManager.CustomerOrderDelivered();
            Destroy(collision.gameObject);
            Wander();
            wander = true;
            SpawnManager.Instance.DespawnEvent.Invoke(gameObject);
        }
        else
        {
            if (wander)
            {
                Wander();
            }
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

    void Wander()
    {
        // does nothing except pick a new destination to go to

        wayPoint = new Vector3(UnityEngine.Random.Range(transform.position.x - Range, transform.position.x + Range), 1, UnityEngine.Random.Range(transform.position.z - Range, transform.position.z + Range))
        {
            y = 0.29f
        };
        // don't need to change direction every frame seeing as you walk in a straight line only
        transform.LookAt(wayPoint);
    }
}
