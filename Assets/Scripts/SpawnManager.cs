using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    //Prefab references
    //Customers
    public GameObject _customersInHierarchy;
    public GameObject _customerPrefab;
    public GameObject[] customerSpawnLocations = { null, null, null, null };
    //Food
    public GameObject _foodInHierarchy;
    public GameObject[] _foodPrefabs = { null, null, null, null, null, null };
    public GameObject[] foodSpawnLocations = { null, null, null };


    List<Customer> _customers = new List<Customer>();
    List<GameObject> _food = new List<GameObject>();

	// Use this for initialization
	void Awake () {
        SpawnInitialCustomers();
        SpawnInitialFood();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SpawnInitialCustomers()
    {
        for (int i = 0; i < customerSpawnLocations.Length; i++)
        {
            GameObject newCustomerObj = Instantiate(_customerPrefab);
            Customer newCustomer = newCustomerObj.GetComponent<Customer>();
            newCustomer.AssignRandomBehavior();
            newCustomerObj.transform.parent = _customersInHierarchy.transform;
            newCustomerObj.transform.name = newCustomer.Behavior.GetType().ToString() + " " + i;
            newCustomerObj.GetComponent<MeshRenderer>().material.color = newCustomer.Behavior.Color;
            newCustomerObj.transform.position = customerSpawnLocations[i].transform.position;
            _customers.Add(newCustomer);
        }
    }

    private void SpawnInitialFood()
    {
        for (int i = 0; i < foodSpawnLocations.Length; i++)
        {
            GameObject newFoodObj = Instantiate(_foodPrefabs[UnityEngine.Random.Range(0, _foodPrefabs.Length)]);
            newFoodObj.transform.parent = _foodInHierarchy.transform;
            newFoodObj.transform.position = foodSpawnLocations[i].transform.position;
            _food.Add(newFoodObj);
        }
    }
}
