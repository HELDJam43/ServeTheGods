using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPoint : SnapPoint
{

    public FoodActions action;
    Timer timer;
    public override void OnPlace()
    {
        base.OnPlace();
        FoodWorldObject food = p as FoodWorldObject;
        if (FoodManager.IsValidRecipe(food.Food, action))
            MakeTimer();
    }
    public override void OnPickedUp()
    {
        base.OnPickedUp();
        if (timer)
            Destroy(timer.gameObject);

    }
    public void OnCookComplete()
    {
        timer = null;
        FoodWorldObject food = p as FoodWorldObject;
        Food output = FoodManager.GetRecipe(food.Food, action).output;
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
}
