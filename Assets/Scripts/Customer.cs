using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {

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
        _behavior = new NeutralCustomer();
    }
}
