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

    public int CustomerRespawnRate = 3;
    public int FoodRespawnRate = 2;
    public int GodSpawnRate = 4;

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

        for (int i = 0; i < level._customerInitialCount; i ++)
        {
            SpawnInitialCustomers();
        }

        for (int j = 0; j < level._foodInitialCount; j++)
        {
            SpawnInitialFood();
        }

        for (int k = 0; k < level._godInitialCount; k++)
        {
            SpawnInitialGods();
        }
    }

    void ScoreAdd(int num)
    {
        Debug.Log(num);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            DespawnEverything();
        }
    }

    private SpawnInventory _inventory = new SpawnInventory();
    private Queue<GameObject> _slotsToFree = new Queue<GameObject>();
    private Level _level;

    private void DespawnSomething(GameObject go)
    {
        _slotsToFree.Enqueue(go);
        Invoke("DelayedRespawn", go.GetComponent<Customer>() == null ? FoodRespawnRate : CustomerRespawnRate);
    }

    private void DelayedRespawn()
    {
        GameObject slot = _slotsToFree.Dequeue();
        if (slot == null)
            return;

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
        _inventory.SetGodChairSlotNum(godSpawnLocations.Length);
    }

    private void SpawnInitialCustomers()
    {
        if (_inventory.ChairSlotOpen())
        {
            GameObject newCustomerObj = Instantiate(_customerPrefabs[UnityEngine.Random.Range(0, _customerPrefabs.Length)]);
            Customer newCustomer = newCustomerObj.AddComponent<Customer>();
            newCustomer.AssignRandomBehavior();
            newCustomerObj.transform.parent = _customersInHierarchy.transform;
            //newCustomerObj.transform.name = newCustomer.Behavior.GetType().ToString();

            int slot = _inventory.TakeChairSlot(newCustomerObj);
            newCustomerObj.transform.position = customerSpawnLocations[slot].transform.position
                - (customerSpawnLocations[slot].transform.forward * .5f)
                + (customerSpawnLocations[slot].transform.right * .5f)
                + (customerSpawnLocations[slot].transform.up * .75f);
            newCustomerObj.transform.forward = -customerSpawnLocations[slot].transform.right;
            Food selectedFood = FoodManager.GetRandomCustomerFood();
            GameObject orderBub = SpawnOrderBubble(selectedFood, newCustomerObj);
            orderBub.transform.localEulerAngles = new Vector3(0, 90, 0);
            newCustomer.SetDesiredFood(selectedFood, orderBub);
        }
    }

    private void SpawnInitialGods()
    {
        if (_inventory.GodChairSlotOpen())
        {
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

    private GameObject SpawnOrderBubble(Food selectedFood, GameObject obj)
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