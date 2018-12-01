using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{

    public List<Food> allFoods;
    public static FoodManager Instance;
    void Awake()
    {
        Instance = this;
    }

    public static Food GetRandomFood()
    {
        return Instance.allFoods[Random.Range(0, Instance.allFoods.Count)];
    }
}
