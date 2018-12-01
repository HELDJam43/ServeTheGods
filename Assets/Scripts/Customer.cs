using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {

    private static int _id = 0;
    public CustomerBehavior Behavior { get { return _behavior; } set { _behavior = value; } }

    // Use this for initialization
    public Customer() {
        AssignRandomBehavior();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private CustomerBehavior _behavior;

    private void AssignRandomBehavior()
    {
        if (TempIsEven(_id++))
        {
            _behavior = new NeutralCustomer();
        }
        else
        {
            _behavior = new AngryCustomer();
        }
    }


    private bool TempIsEven(int num)
    {
        if (num % 2 == 0)
            return true;
        else
            return false;
    }
}
