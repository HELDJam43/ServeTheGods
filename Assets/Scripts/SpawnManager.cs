using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyIntEvent : UnityEvent<GameObject>
{
}

public class SpawnManager : MonoBehaviour {

    //Prefab references
    //Customers
    public GameObject _customersInHierarchy;
    public GameObject[] _customerPrefabs = { null, null, null, null };
    public GameObject[] customerSpawnLocations = { null, null, null, null };
    //Food
    public GameObject _foodInHierarchy;
    public GameObject[] _foodPrefabs = { null, null, null, null, null, null };
    public GameObject[] foodSpawnLocations = { null, null, null };

    public static SpawnManager Instance;
    public MyIntEvent DespawnEvent;
    // Use this for initialization
    void Awake () {
        Instance = this;
        _inventory.SetChairSlotNum(customerSpawnLocations.Length);
        _inventory.SetFoodSlotNum(foodSpawnLocations.Length);
    }

    private void Start()
    {
        if (DespawnEvent == null)
            DespawnEvent = new MyIntEvent();

        DespawnEvent.AddListener(DespawnSomething);
    }

    void ScoreAdd(int num)
    {
        Debug.Log(num);
    }

    // Update is called once per frame
    void Update () {
        SpawnInitialCustomers();
        SpawnInitialFood();

        if (Input.GetKeyUp(KeyCode.R))
        {
            DespawnEverything();
        }
    }

    private SpawnInventory _inventory = new SpawnInventory();

    private void DespawnSomething(GameObject go)
    {
        if (go.GetComponent<Customer>() == null)
        {
            _inventory.FreeFoodSlot(go);
        }
        else
        {
            _inventory.FreeChairSlot(go);
        }
    }

    private void DespawnEverything()
    {
        _inventory.FreeAllSlots();
        _inventory.SetChairSlotNum(customerSpawnLocations.Length);
        _inventory.SetFoodSlotNum(foodSpawnLocations.Length);
    }

    private void SpawnInitialCustomers()
    {
        if (_inventory.ChairSlotOpen())
        {
            GameObject newCustomerObj = Instantiate(_customerPrefabs[UnityEngine.Random.Range(0, _customerPrefabs.Length)]);
            Customer newCustomer = newCustomerObj.AddComponent<Customer>();
            newCustomer.AssignRandomBehavior();
            newCustomerObj.transform.parent = _customersInHierarchy.transform;
            newCustomerObj.transform.name = newCustomer.Behavior.GetType().ToString();
           // newCustomerObj.GetComponent<MeshRenderer>().material.color = newCustomer.Behavior.Color;

            int slot = _inventory.TakeChairSlot(newCustomerObj);
            newCustomerObj.transform.position = customerSpawnLocations[slot].transform.position;
        }
    }

    private void SpawnInitialFood()
    {
        if (_inventory.FoodSlotOpen())
        {
            GameObject newFoodObj = Instantiate(_foodPrefabs[UnityEngine.Random.Range(0, _foodPrefabs.Length)]);
            newFoodObj.transform.parent = _foodInHierarchy.transform;

            int slot = _inventory.TakeFoodSlot(newFoodObj);
            newFoodObj.transform.position = foodSpawnLocations[slot].transform.position;
        }
    }
}