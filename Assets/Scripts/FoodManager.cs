using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public List<FoodRecipes> recipes;
    public List<Food> customerFoods;
    List<Food> customerFoodsNonRecipe;
    public List<Food> godFoods;
    List<Food> godFoodsNonRecipe;
    public static FoodManager Instance;
    Dictionary<Food, List<FoodRecipes>> recipeDic;
    void Awake()
    {
        Instance = this;
        recipeDic = new Dictionary<Food, List<FoodRecipes>>();
        customerFoodsNonRecipe = new List<Food>();
        godFoodsNonRecipe = new List<Food>();
        foreach (Food f in customerFoods)
        {
            if (!f.IsRecipe)
                customerFoodsNonRecipe.Add(f);
        }
        foreach (Food f in godFoods)
        {
            if (!f.IsRecipe)
                godFoodsNonRecipe.Add(f);
        }
        foreach (FoodRecipes foodRecipe in recipes)
        {
            if (!recipeDic.ContainsKey(foodRecipe.input))
            {
                recipeDic.Add(foodRecipe.input, new List<FoodRecipes>());
            }
            recipeDic[foodRecipe.input].Add(foodRecipe);
        }
    }

    public static Food GetRandomCustomerFood(bool excludeRecipes)
    {
        if (excludeRecipes)
        {
            return Instance.customerFoodsNonRecipe[Random.Range(0, Instance.customerFoodsNonRecipe.Count)];
        }
        return Instance.customerFoods[Random.Range(0, Instance.customerFoods.Count)];
    }
    public static Food GetRandomGodFood(bool excludeRecipes)
    {
        if (excludeRecipes)
        {
            return Instance.godFoodsNonRecipe[Random.Range(0, Instance.godFoodsNonRecipe.Count)];
        }
        return Instance.godFoods[Random.Range(0, Instance.godFoods.Count)];
    }
    public static bool IsValidRecipe(Food i, FoodActions a)
    {
        if (!Instance.recipeDic.ContainsKey(i)) return false;
        foreach (FoodRecipes recipe in Instance.recipeDic[i])
        {
            if (recipe.action == a)
                return true;
        }
        return false;
    }

    public static FoodRecipes GetRecipe(Food i, FoodActions a)
    {
        if (!Instance.recipeDic.ContainsKey(i)) return null;
        foreach (FoodRecipes recipe in Instance.recipeDic[i])
        {
            if (recipe.action == a)
                return recipe;
        }
        return null;
    }

}
