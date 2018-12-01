using UnityEngine;

public abstract class CustomerBehavior 
{
    //orders
    //ai
    //type
    //movement

    public enum Attitude { Neutral,Happy,Mild,Angry}
    protected Attitude startingAttitude;
    protected Attitude currentAttitude;
    public Color Color;
    public GameObject Prefab;
}

public class NeutralCustomer: CustomerBehavior
{
    public NeutralCustomer()
    {
        startingAttitude = Attitude.Neutral;
        currentAttitude = startingAttitude;
        Color = Color.gray;
    }
}

public class HappyCustomer : CustomerBehavior
{
    public HappyCustomer()
    {
        startingAttitude = Attitude.Neutral;
        currentAttitude = startingAttitude;
        Color = Color.green;
    }
}

public class MildCustomer : CustomerBehavior
{
    public MildCustomer()
    {
        startingAttitude = Attitude.Neutral;
        currentAttitude = startingAttitude;
        Color = Color.yellow;
    }
}

public class AngryCustomer : CustomerBehavior
{
    public AngryCustomer()
    {
        startingAttitude = Attitude.Angry;
        currentAttitude = startingAttitude;
        Color = Color.red;
    }
}
