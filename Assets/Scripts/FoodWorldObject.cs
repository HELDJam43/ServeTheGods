using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodWorldObject : PickupAble
{
    [SerializeField]
    Food food;

    public Food Food
    {
        get
        {
            return food;
        }
    }
}
