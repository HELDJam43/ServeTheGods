using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Recipe", menuName = "Food/Make Recipe", order = 2)]
public class FoodRecipes : ScriptableObject
{
    public Food input, output;
    public FoodActions action;
}
