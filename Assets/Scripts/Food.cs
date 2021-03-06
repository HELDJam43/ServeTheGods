﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Food/Make Food", order = 2)]
public class Food : ScriptableObject
{
    public string foodName;
    public GameObject foodPrefab;
    public Mesh foodMesh;
    public string[] friendlyNames;
    public bool IsRecipe;
}

public enum FoodActions
{
    NONE,
    CHOP,
    COOK,
    BOIL
}
