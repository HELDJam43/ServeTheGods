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
    //Order Bubble
    public GameObject _orderPrefab;

    public int CustomerRespawnRate = 3;
    public int FoodRespawnRate = 2;

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
    private Queue<GameObject> _slotsToFree = new Queue<GameObject>();

    private void DespawnSomething(GameObject go)
    {
        _slotsToFree.Enqueue(go);
        Invoke("DelayedRespawn", go.GetComponent<Customer>() == null ? FoodRespawnRate : CustomerRespawnRate);
    }

    private void DelayedRespawn()
    {
        GameObject slot = _slotsToFree.Dequeue();
        if (slot.GetComponent<Customer>() == null)
        {
            _inventory.FreeFoodSlot(slot);
        }
        else
        {
            _inventory.FreeChairSlot(slot);
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

            int slot = _inventory.TakeChairSlot(newCustomerObj);
            newCustomerObj.transform.position = customerSpawnLocations[slot].transform.position;

            newCustomer.SetDesiredFood(SpawnOrderBubble(newCustomerObj));
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

    private Food SpawnOrderBubble(GameObject obj)
    {
        GameObject newOrderBubble = Instantiate(_orderPrefab, obj.transform, false);
        newOrderBubble.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + 1, obj.transform.position.z);

        GameObject foodType = newOrderBubble.transform.Find("FoodType").gameObject;
        Food selectedFood = FoodManager.GetRandomFood();
        foodType.GetComponentInChildren<MeshFilter>().sharedMesh = selectedFood.foodMesh;
        return selectedFood;
    }
}