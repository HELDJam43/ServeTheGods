public abstract class CustomerBehavior 
{
    //orders
    //ai
    //type
    //movement

    public enum Attitude { Neutral,Happy,Mild,Angry}
    protected Attitude startingAttitude;
    protected Attitude currentAttitude;
}

public class NeutralCustomer: CustomerBehavior
{
    public NeutralCustomer()
    {
        startingAttitude = Attitude.Neutral;
        currentAttitude = startingAttitude;      
    }
}
