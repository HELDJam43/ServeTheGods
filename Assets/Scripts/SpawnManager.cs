using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyIntEvent : UnityEvent<GameObject>
{
}

public class MyEmptyEvent : UnityEvent<GameObject>
{
}

public class SpawnManager : MonoBehaviour
{

    #region Prefab References
    //Customers
    public GameObject _customersInHierarchy;
    public GameObject[] _customerPrefabs = { null, null, null, null };
    public GameObject[] customerSpawnLocations = { null, null, null, null };
    //Food
    public GameObject _foodInHierarchy;
    public GameObject[] _foodPrefabs = { null, null, null, null, null, null };
    public GameObject[] foodSpawnLocations = { null, null, null };
    //Gods
    public GameObject _godsInHierarchy;
    public GameObject[] _godPrefabs = { null, null, null, null, null };
    public GameObject[] godSpawnLocations = { null, null, null };
    //Order Bubble
    public GameObject _orderPrefab;
    #endregion

    private int _currentCustomerCount = 0;
    private int _currentFoodCount = 0;
    private int _currentGodCount = 0;

    public static SpawnManager Instance;
    public MyIntEvent DespawnEvent;
    public MyEmptyEvent ResetFoodEvent;
    // Use this for initialization
    void Awake()
    {
        Instance = this;
        _inventory.SetChairSlotNum(customerSpawnLocations.Length);
        _inventory.SetFoodSlotNum(foodSpawnLocations.Length);
        _inventory.SetGodChairSlotNum(godSpawnLocations.Length);
    }

    private void Start()
    {
        if (DespawnEvent == null)
            DespawnEvent = new MyIntEvent();

        if (ResetFoodEvent == null)
            ResetFoodEvent = new MyEmptyEvent();

        DespawnEvent.AddListener(DespawnSomething);
        ResetFoodEvent.AddListener(ResetFood);
    }

    public void Init(Level level)
    {
        _level = level;

        for (int i = 0; i < level._customerInitialCount; i++)
        {
            SpawnInitialCustomers(true);
        }

        for (int j = 0; j < level._foodInitialCount; j++)
        {
            SpawnInitialFood();
        }

        for (int k = 0; k < level._godInitialCount; k++)
        {
            SpawnInitialGods();
        }

        _currentCustomerCount = level._customerInitialCount;
        _currentFoodCount = level._foodInitialCount;
        _currentGodCount = level._godInitialCount;
    }

    void ScoreAdd(int num)
    {
        Debug.Log(num);
    }

    // Update is called once per frame
    void Update()
    {

        SpawnCustomers();
        SpawnFood();
        SpawnGod();

        if (Input.GetKeyUp(KeyCode.R))
        {
            DespawnEverything();
        }
    }

    private SpawnInventory _inventory = new SpawnInventory();
    private Queue<GameObject> _slotsToFree = new Queue<GameObject>();
    private Level _level;
    bool _custermerSpawnWaiting = false;
    bool _foodSpawnWaiting = false;
    bool _godSpawnWaiting = false;

    private void DespawnSomething(GameObject go)
    {
        _slotsToFree.Enqueue(go);
        Invoke("DelayedRespawn", go.GetComponent<Customer>() == null ? _level._foodSpawnRate : _level._customerSpawnRate);
    }

    private void DelayedRespawn()
    {
        GameObject slot = _slotsToFree.Dequeue();
        if (slot == null)
            return;

        if (slot.GetComponent<Customer>() == null)
        {
            SpawnInitialFood();
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
        _inventory.SetGodChairSlotNum(godSpawnLocations.Length);
    }

    private void SpawnInitialCustomers(bool isInitial)
    {
        _custermerSpawnWaiting = false;
        if (_inventory.ChairSlotOpen() && _currentCustomerCount < _level._customerTotalCount)
        {
            _currentCustomerCount++;
            GameObject newCustomerObj = Instantiate(_customerPrefabs[UnityEngine.Random.Range(0, _customerPrefabs.Length)]);
            Customer newCustomer = newCustomerObj.AddComponent<Customer>();
            newCustomer.AssignRandomBehavior();
            newCustomerObj.transform.parent = _customersInHierarchy.transform;
            //newCustomerObj.transform.name = newCustomer.Behavior.GetType().ToString();

            int slot = _inventory.TakeChairSlot(newCustomerObj);
            newCustomer.WalkToInitialLocation(Waypoints.Waypoint.ClosestWaypointTo(customerSpawnLocations[slot].transform.position), isInitial);
        }
    }


    private void SpawnC()
    {
        _custermerSpawnWaiting = false;
        if (_inventory.ChairSlotOpen() && _currentCustomerCount < _level._customerTotalCount)
        {
            _currentCustomerCount++;
            GameObject newCustomerObj = Instantiate(_customerPrefabs[UnityEngine.Random.Range(0, _customerPrefabs.Length)]);
            Customer newCustomer = newCustomerObj.AddComponent<Customer>();
            newCustomer.AssignRandomBehavior();
            newCustomerObj.transform.parent = _customersInHierarchy.transform;
            //newCustomerObj.transform.name = newCustomer.Behavior.GetType().ToString();

            int slot = _inventory.TakeChairSlot(newCustomerObj);
            newCustomer.WalkToInitialLocation(Waypoints.Waypoint.ClosestWaypointTo(customerSpawnLocations[slot].transform.position), false);
        }
    }

    private void SpawnCustomers()
    {
        if (_custermerSpawnWaiting) return;
        _custermerSpawnWaiting = true;
        Invoke("SpawnC", _level._customerSpawnRate);
    }

    private void SpawnInitialGods()
    {
        _godSpawnWaiting = false;
        if (_inventory.GodChairSlotOpen() && _currentGodCount < _level._godTotalCount)
        {
            _currentGodCount++;
            GameObject newGodObj = Instantiate(_godPrefabs[UnityEngine.Random.Range(0, _godPrefabs.Length)]);
            God newGod = newGodObj.AddComponent<God>();
            newGodObj.transform.parent = _godsInHierarchy.transform;

            int slot = _inventory.TakeGodChairSlot(newGodObj);
            newGodObj.transform.position = godSpawnLocations[slot].transform.position;
            //newGodObj.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            Food selectedFood = FoodManager.GetRandomGodFood();
            GameObject orderBubble = SpawnOrderBubble(selectedFood, newGodObj);
            orderBubble.transform.position = new Vector3(orderBubble.transform.position.x + 0.5f, orderBubble.transform.position.y, orderBubble.transform.position.z - 0.7f);
            newGod.SetDesiredFood(selectedFood, orderBubble);
        }
    }

    private void SpawnGod()
    {
        if (_godSpawnWaiting) return;
        _godSpawnWaiting = true;
        Invoke("SpawnInitialGods", _level._godSpawnRate);
    }

    private void SpawnInitialFood()
    {
        _foodSpawnWaiting = false;
        if (_inventory.FoodSlotOpen() && _currentFoodCount < _level._foodTotalCount)
        {
            _currentFoodCount++;
            GameObject newFoodObj = Instantiate(_foodPrefabs[UnityEngine.Random.Range(0, _foodPrefabs.Length)]);
            newFoodObj.transform.parent = _foodInHierarchy.transform;

            int slot = _inventory.TakeFoodSlot(newFoodObj);
            newFoodObj.transform.position = foodSpawnLocations[slot].transform.position;
        }
    }

    private void SpawnFood()
    {
        if (_foodSpawnWaiting) return;
        _foodSpawnWaiting = true;
        Invoke("SpawnInitialFood", _level._foodSpawnRate);
    }

    public GameObject SpawnOrderBubble(Food selectedFood, GameObject obj)
    {
        GameObject newOrderBubble = Instantiate(_orderPrefab, obj.transform, false);
        newOrderBubble.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + 1, obj.transform.position.z);
        GameObject foodType = newOrderBubble.transform.Find("FoodType").gameObject;

        foodType.GetComponentInChildren<MeshFilter>().sharedMesh = selectedFood.foodMesh;
        return newOrderBubble;
    }

    private void ResetFood(GameObject go)
    {
        God script = go.GetComponent<God>();
        Food selectedFood = FoodManager.GetRandomGodFood();
        GameObject orderBubble = SpawnOrderBubble(selectedFood, go);
        orderBubble.transform.position = new Vector3(orderBubble.transform.position.x + 0.5f, orderBubble.transform.position.y, orderBubble.transform.position.z - 0.7f);
        script.SetDesiredFood(selectedFood, orderBubble);
    }
}