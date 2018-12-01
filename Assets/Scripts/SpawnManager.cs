using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    //Prefab references
    public GameObject _customersInHierarchy;
    public GameObject _customerPrefab;
    public GameObject[] customerSpawnLocations = { null, null, null, null };


    List<Customer> _customers = new List<Customer>();

	// Use this for initialization
	void Awake () {

       // SpawnInitialCustomers();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SpawnInitialCustomers()
    {
        GameObject newCustomerObj = Instantiate(_customerPrefab);
        Customer newCustomer = newCustomerObj.GetComponent<Customer>();
        newCustomerObj.transform.parent = _customersInHierarchy.transform;
        newCustomerObj.transform.name = newCustomer.Behavior.GetType().ToString();
        _customers.Add(newCustomer);
    }
}
