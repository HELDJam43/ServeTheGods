using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {

    public CustomerBehavior Behavior { get { return _behavior; } set { _behavior = value; } }

    // Use this for initialization
    public Customer() {

	}

    // Update is called once per frame
    void Update () {
		
	}

    private CustomerBehavior _behavior;

    public void AssignRandomBehavior()
    {
        CustomerBehavior.Attitude attitude = TempRanBehavior();
        switch (attitude)
        {
            case CustomerBehavior.Attitude.Neutral:
                {
                    _behavior = new NeutralCustomer();
                    break;
                }
            case CustomerBehavior.Attitude.Happy:
                {
                    _behavior = new HappyCustomer();
                    break;
                }
            case CustomerBehavior.Attitude.Mild:
                {
                    _behavior = new MildCustomer();
                    break;
                }
            case CustomerBehavior.Attitude.Angry:
                {
                    _behavior = new AngryCustomer();
                    break;
                }
        }
    }

    private CustomerBehavior.Attitude TempRanBehavior()
    {
        Array values = Enum.GetValues(typeof(CustomerBehavior.Attitude));
        int x = UnityEngine.Random.Range(0, values.Length);
        return (CustomerBehavior.Attitude)values.GetValue(x);
    }
}
