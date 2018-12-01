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
}

public class NeutralCustomer: CustomerBehavior
{
    public NeutralCustomer()
    {
        startingAttitude = Attitude.Neutral;
        currentAttitude = startingAttitude;
        Color = Color.green;
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
