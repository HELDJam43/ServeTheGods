using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{

    public CustomerBehavior Behavior { get { return _behavior; } set { _behavior = value; } }
    public Food DesiredFood;
    public GameObject OrderBubble;

    private Vector3 wayPoint;
    private Timer eatingTimer;
    private WaypointMovement wm;
    AIState state = AIState.WAITING;

    string eatenFood = "hungry";

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
        if (transform.position.y >= 1)
        {
            if (state == AIState.EATING)
            {
                eatingTimer.StopTimer();
            }
            if (state != AIState.HELD)
            {
                Destroy(OrderBubble);
                state = AIState.HELD;
                if (wm)
                    wm.StopMoving(true);
            }
        }
        else
            if (state == AIState.HELD && transform.position.y < .3f)
        {
            OnScared();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != AIState.WAITING) return;
        FoodWorldObject collidedFood = collision.collider.GetComponent<FoodWorldObject>();
        if (collidedFood != null && collidedFood.Food.foodName.ToLower() == DesiredFood.foodName.ToLower())
        {
            eatenFood = DesiredFood.friendlyNames[UnityEngine.Random.Range(0, DesiredFood.friendlyNames.Length)];
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
        eatingTimer = obj.GetComponent<Timer>();
        eatingTimer.StartTimer(15, transform, 2, OnFinisheEating);

    }
    void OnFinisheEating()
    {
        if (wm == null)
            wm = gameObject.AddComponent<WaypointMovement>();
        wm.StartMoving(null, Global.EntranceWaypoint, Despawn, OnLeave, 0.05f);
        state = AIState.LEAVING;
        PostManager.INSTANCE.MakePost(PostRandomizer.RatingType.Fed, eatenFood);
    }
    void OnScared()
    {
        if (wm == null)
            wm = gameObject.AddComponent<WaypointMovement>();
        wm.StartMoving(null, Global.EntranceWaypoint, Despawn, OnLeave, 1);
        state = AIState.SCARED;
    }
    void Despawn(Waypoints.Waypoint w)
    {
        SpawnManager.Instance.DespawnEvent.Invoke(gameObject);
    }

    void OnLeave(Waypoints.Waypoint w)
    {
        Destroy(this.gameObject);
    }
    public void WalkToInitialLocation(Waypoints.Waypoint dest, bool isInitial)
    {
        if (isInitial)
        {
            ReachDestination(dest);
            return;
        }
        state = AIState.ARRIVING;
        Vector3 offset = UnityEngine.Random.onUnitSphere * 4;
        offset.y = 0;
        transform.position = Global.Spawn.transform.position + offset;
        if (wm == null)
            wm = gameObject.AddComponent<WaypointMovement>();
        wm.StartMoving(Global.EntranceWaypoint, dest, null, ReachDestination, 0);
    }
    void ReachDestination(Waypoints.Waypoint w)
    {
        if (wm)
            wm.StopMoving(false);
        state = AIState.WAITING;
        Vector3 vec = w.transform.position
                - (w.transform.forward * .375f)
                - (w.transform.right * .375f);
        vec.y = 0.5f;
        transform.position = vec;

        transform.forward = -w.transform.right;
        Food selectedFood = FoodManager.GetRandomCustomerFood();
        GameObject orderBub = SpawnManager.Instance.SpawnOrderBubble(selectedFood, gameObject);
        orderBub.transform.localEulerAngles = new Vector3(0, 90, 0);
        SetDesiredFood(selectedFood, orderBub);
        if (Global.FirstCustomerSit)
        {
            Global.FirstCustomerSit = false;
            TutorialText tt = Instantiate(Global.TutorialPrefab).GetComponent<TutorialText>();
            tt.Show("Serve food to customers to increase your restuarants rating.\n Reach 5 stars to complete the level", 5, transform.position, 1.5f, 7);
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
