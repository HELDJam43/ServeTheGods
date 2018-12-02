using System.Collections.Generic;
using UnityEngine;

public class SpawnInventory
{

    public Dictionary<int, GameObject> _foodSlot = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> _chairSlot = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> _godChairSlot = new Dictionary<int, GameObject>();

    public void FreeAllSlots()
    {
        foreach (KeyValuePair<int, GameObject> objs in _foodSlot)
        {
            if (objs.Value != null)
            {
               GameObject.Destroy(objs.Value);
            }
        }

        foreach (KeyValuePair<int, GameObject> objs in _chairSlot)
        {
            if (objs.Value != null)
            {
                GameObject.Destroy(objs.Value);
            }
        }

        foreach (KeyValuePair<int, GameObject> objs in _godChairSlot)
        {
            if (objs.Value != null)
            {
                GameObject.Destroy(objs.Value);
            }
        }
    }

    public void SetFoodSlotNum(int num)
    {
        for (int i = 0; i < num; i++)
        {
            if (!_foodSlot.ContainsKey(i))
            _foodSlot.Add(i, null);
        }
    }

    public bool FoodSlotOpen()
    {

        foreach (KeyValuePair<int, GameObject> objs in _foodSlot)
        {
            if (objs.Value == null)
            {
                return true;
            }
        }
        return false;
    }

    public int TakeFoodSlot(GameObject foodGO)
    {
        int slot = -1;

        foreach(KeyValuePair<int,GameObject> objs in _foodSlot)
        {
            if (objs.Value == null)
            {
                slot = objs.Key;
            }
        }

        if (slot != -1)
        {
            _foodSlot[slot] = foodGO;
        }

        return slot;
    }

    public void FreeFoodSlot(GameObject go)
    {
        int slot = -1;
        foreach (KeyValuePair<int, GameObject> objs in _foodSlot)
        {
            if (objs.Value == go)
            {
                slot = objs.Key;

            }
        }

        if (slot != -1)
        {
            _foodSlot[slot] = null;
        }
    }

    public void SetChairSlotNum(int num)
    {
        for (int i = 0; i < num; i++)
        {
            if (!_chairSlot.ContainsKey(i))
            _chairSlot.Add(i, null);
        }
    }

    public bool ChairSlotOpen()
    {

        foreach (KeyValuePair<int, GameObject> objs in _chairSlot)
        {
            if (objs.Value == null)
            {
                return true;
            }
        }
        return false;
    }

    public void FreeChairSlot(GameObject go)
    {
        int slot = -1;
        foreach (KeyValuePair<int, GameObject> objs in _chairSlot)
        {
            if (objs.Value == go)
            {
                slot = objs.Key;

            }
        }

        if (slot != -1)
        {
            _chairSlot[slot] = null;
        }
    }

    public int TakeChairSlot(GameObject chairGO)
    {
        int slot = -1;

        foreach (KeyValuePair<int, GameObject> objs in _chairSlot)
        {
            if (objs.Value == null)
            {
                slot = objs.Key;
            }
        }

        if (slot != -1)
        {
           _chairSlot[slot] = chairGO;
        }

        return slot;
    }

    public void SetGodChairSlotNum(int num)
    {
        for (int i = 0; i < num; i++)
        {
            if (!_godChairSlot.ContainsKey(i))
                _godChairSlot.Add(i, null);
        }
    }

    public bool GodChairSlotOpen()
    {

        foreach (KeyValuePair<int, GameObject> objs in _godChairSlot)
        {
            if (objs.Value == null)
            {
                return true;
            }
        }
        return false;
    }

    public void FreeGodChairSlot(GameObject go)
    {
        int slot = -1;
        foreach (KeyValuePair<int, GameObject> objs in _godChairSlot)
        {
            if (objs.Value == go)
            {
                slot = objs.Key;

            }
        }

        if (slot != -1)
        {
            _godChairSlot[slot] = null;
        }
    }

    public int TakeGodChairSlot(GameObject chairGO)
    {
        int slot = -1;

        foreach (KeyValuePair<int, GameObject> objs in _godChairSlot)
        {
            if (objs.Value == null)
            {
                slot = objs.Key;
            }
        }

        if (slot != -1)
        {
            _godChairSlot[slot] = chairGO;
        }

        return slot;
    }
}
