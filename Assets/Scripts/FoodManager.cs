using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{

    List<Food> allFoods;
    public List<Food> customerFoods;
    public List<Food> godFoods;
    public static FoodManager Instance;
    void Awake()
    {
        allFoods = new List<Food>();
        foreach (Food f in customerFoods)
        {
            allFoods.Add(f);
        }
        foreach (Food f in godFoods)
        {
            allFoods.Add(f);
        }
        Instance = this;
    }

    public static Food GetRandomFood()
    {
        return Instance.allFoods[Random.Range(0, Instance.allFoods.Count)];
    }
    public static Food GetRandomCustomerFood()
    {
        return Instance.customerFoods[Random.Range(0, Instance.customerFoods.Count)];
    }
    public static Food GetRandomGodFood()
    {
        return Instance.godFoods[Random.Range(0, Instance.godFoods.Count)];
    }
}
