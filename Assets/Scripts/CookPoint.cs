using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPoint : SnapPoint
{

    public FoodActions action;
    Timer timer;
    Pan pan;
    CuttingBoard cutboard;
    Food output;
    void Start()
    {
        pan = GetComponentInParent<Pan>();
        cutboard = GetComponentInParent<CuttingBoard>();
    }
    public override void OnPlace()
    {
        base.OnPlace();
        FoodWorldObject food = p as FoodWorldObject;

        if (FoodManager.IsValidRecipe(food.Food, action))
        {
            MakeTimer();
            PlayAnim();
            output = FoodManager.GetRecipe(food.Food, action).output;
        }
        else
        {
            if (action == FoodActions.COOK && food.Food != Global.Ashes)
            {
                MakeTimer();
                PlayAnim();
                output = Global.Ashes;
            }
        }
    }
    public override void OnPickedUp()
    {
        base.OnPickedUp();
        StopAnim();
        if (timer)
            Destroy(timer.gameObject);

    }
    public void OnCookComplete()
    {
        StopAnim();
        timer = null;
        FoodWorldObject food = p as FoodWorldObject;

        PickupAble temp = GameObject.Instantiate(output.foodPrefab).GetComponent<PickupAble>();
        p.Release();
        p.sp = null;
        IsOccupied = false;
        Destroy(p.gameObject);
        Attach(temp);
    }
    private void MakeTimer()
    {
        GameObject obj = Instantiate(Global.TimerPrefab);
        timer = obj.GetComponent<Timer>();
        timer.StartTimer(5, transform, 2, OnCookComplete);
    }
    void PlayAnim()
    {

        if (pan)
            pan.activated = true;
        if (cutboard)
            cutboard.activated = true;
    }

    void StopAnim()
    {
        if (pan)
            pan.activated = false;
        if (cutboard)
            cutboard.activated = false;
    }
}
