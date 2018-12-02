using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    public Food DesiredFood;
    public GameObject OrderBubble;
    public Timer DesireTimer;

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
        GameObject obj = Instantiate(Global.TimerPrefab,gameObject.transform);
        DesireTimer = obj.GetComponent<Timer>();
        DesireTimer.StartTimer(UnityEngine.Random.Range(50, 100), transform, 2.7f, HandleOnTimerComplete);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains(DesiredFood.name.ToLower()))
        {
            //TODO MORE here instead of just getting rid of bubble
            Destroy(OrderBubble);
            LevelManager.GodOrderDelivered();
            Destroy(collision.gameObject);
            ResetDesiredFood();
        }
    }

    public void SetDesiredFood(Food desiredFood, GameObject orderBubble)
    {
        DesiredFood = desiredFood;
        OrderBubble = orderBubble;
    }

    public void ResetDesiredFood()
    {
        Destroy(DesireTimer);
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
