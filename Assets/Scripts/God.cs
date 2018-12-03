using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    public Food DesiredFood;
    public GameObject OrderBubble;
    public Timer DesireTimer;
    private Timer eatingTimer;
    GodState state = GodState.WAITING;
    public enum GodState
    {
        WAITING,
        EATING,
    }
    // Use this for initialization
    public God()
    {

    }
    private void Awake()
    {
        MakeTimer();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void MakeTimer()
    {
        GameObject obj = Instantiate(Global.TimerPrefab, gameObject.transform);
        DesireTimer = obj.GetComponent<Timer>();
        DesireTimer.StartTimer(UnityEngine.Random.Range(20, 50), transform, 2.7f, HandleOnTimerComplete);
    }

    private void OnCollisionEnter(Collision collision)
    {
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
        state = GodState.EATING;
        LevelManager.GodOrderDelivered();
        GameObject obj = Instantiate(Global.TimerPrefab);
        eatingTimer = obj.GetComponent<Timer>();
        Destroy(DesireTimer.gameObject);
        eatingTimer.StartTimer(UnityEngine.Random.Range(10, 30), transform, 2.7f, OnFinisheEating);
    }
    void OnFinisheEating()
    {
        ResetDesiredFood();
    }
    public void SetDesiredFood(Food desiredFood, GameObject orderBubble)
    {
        DesiredFood = desiredFood;
        OrderBubble = orderBubble;
    }

    public void ResetDesiredFood()
    {
        
        MakeTimer();
        SpawnManager.Instance.ResetFoodEvent.Invoke(gameObject);
    }

    void HandleOnTimerComplete()
    {
        Destroy(OrderBubble);
        LevelManager.GodOrderFailed();
        ResetDesiredFood();
    }
}
