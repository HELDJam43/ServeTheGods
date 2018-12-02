using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyIntEvent : UnityEvent<GameObject>
{
}

public class SpawnManager : MonoBehaviour {

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
    public GameObject[] _godPrefabs = { null, null, null, null, null};
    public GameObject[] godSpawnLocations = { null, null, null };
    //Order Bubble
    public GameObject _orderPrefab;
    #endregion

    public int CustomerRespawnRate = 3;
    public int FoodRespawnRate = 2;

    public static SpawnManager Instance;
    public MyIntEvent DespawnEvent;
    // Use this for initialization
    void Awake () {
        Instance = this;
        _inventory.SetChairSlotNum(customerSpawnLocations.Length);
        _inventory.SetFoodSlotNum(foodSpawnLocations.Length);
        _inventory.SetGodChairSlotNum(godSpawnLocations.Length);
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
        SpawnInitialGods();

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
            newCustomerObj.transform.name = newCustomer.Behavior.GetType().ToString();

            int slot = _inventory.TakeChairSlot(newCustomerObj);
            newCustomerObj.transform.position = customerSpawnLocations[slot].transform.position;

            Food selectedFood = FoodManager.GetRandomFood();
            newCustomer.SetDesiredFood(selectedFood,SpawnOrderBubble(selectedFood,newCustomerObj));
        }
    }

    private void SpawnInitialGods()
    {
        if (_inventory.GodChairSlotOpen())
        {
            GameObject newGodObj = Instantiate(_godPrefabs[UnityEngine.Random.Range(0, _godPrefabs.Length)]);
            //Customer newCustomer = newCustomerObj.AddComponent<Customer>();
            //newCustomer.AssignRandomBehavior();
            //newCustomerObj.transform.parent = _customersInHierarchy.transform;
            //newCustomerObj.transform.name = newCustomer.Behavior.GetType().ToString();

            int slot = _inventory.TakeGodChairSlot(newGodObj);
            newGodObj.transform.position = godSpawnLocations[slot].transform.position;
            newGodObj.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            Food selectedFood = FoodManager.GetRandomFood();
            //newGodObj.SetDesiredFood(selectedFood, SpawnOrderBubble(selectedFood, newCustomerObj));
            GameObject orderBubble = SpawnOrderBubble(selectedFood, newGodObj);
            orderBubble.transform.position = new Vector3(orderBubble.transform.position.x + 0.5f, orderBubble.transform.position.y, orderBubble.transform.position.z - 0.7f);
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

    private GameObject SpawnOrderBubble(Food selectedFood,GameObject obj)
    {
        GameObject newOrderBubble = Instantiate(_orderPrefab, obj.transform, false);
        newOrderBubble.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + 1, obj.transform.position.z);

        GameObject foodType = newOrderBubble.transform.Find("FoodType").gameObject;
      
        foodType.GetComponentInChildren<MeshFilter>().sharedMesh = selectedFood.foodMesh;
        return newOrderBubble;
    }
}