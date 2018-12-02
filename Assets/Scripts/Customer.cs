using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{

    public CustomerBehavior Behavior { get { return _behavior; } set { _behavior = value; } }
    public Food DesiredFood;
    public GameObject OrderBubble;

    private bool wander = false;
    private float Speed = 0.5f;
    private Vector3 wayPoint;
    private float Range = 5;
    AIState state = AIState.WAITING;

    public enum AIState
    {
        WAITING,
        EATING,
        LEAVING,
        SCARED,
        HELD,
        ARRIVING,
        WANDER
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (state != AIState.WAITING) return;
        if (collision.collider.name.Contains(DesiredFood.name.ToLower()))
        {
            //TODO MORE here instead of just getting rid of bubble
            Destroy(OrderBubble);
            Destroy(collision.gameObject);
            OnRecieveFood();
        }
    }
    void OnRecieveFood()
    {
        state = AIState.EATING;
        LevelManager.CustomerOrderDelivered();
        GameObject obj = Instantiate(Global.TimerPrefab);
        Timer DesireTimer = obj.GetComponent<Timer>();
        DesireTimer.StartTimer(15, transform, 2, OnFinisheEating);

    }
    void OnFinisheEating()
    {
        WaypointMovement wm = gameObject.AddComponent<WaypointMovement>();
        wm.StartMoving(null, Global.EntranceWaypoint, Despawn, null);
        state = AIState.LEAVING;
    }
    void Despawn()
    {
        SpawnManager.Instance.DespawnEvent.Invoke(gameObject);
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

    public void SetDesiredFood(Food desiredFood, GameObject orderBubble)
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
