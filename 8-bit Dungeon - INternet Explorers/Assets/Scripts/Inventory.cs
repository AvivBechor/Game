using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Item test;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown("a"))
        {
            Debug.Log("CLICK");
            AddItem(test);
        }
    }

    public void AddItem(Item item)
    {
        if (item != null) {
            var res = inventoryPanel
                    .GetComponentsInChildren<ItemContainerScript>()
                    .FirstOrDefault(i => compareItemType(i, item));

            Debug.Log("res" + res);
            if (res)
            {
                res.count++;
            }
            else
            {
                var emptySlot = inventoryPanel.GetComponentsInChildren<ItemContainerScript>()
                    .Where(i => i.item == null)
                    .FirstOrDefault();
                if(emptySlot)
                {
                    emptySlot.count = 1;
                    emptySlot.item = item;
                } else
                {
                    Debug.Log("Inventory is full");
                }
            }
        }
    }

    public bool compareItemType(ItemContainerScript first, Item second)
    {
        if(first.item == null)
        {
            Debug.Log("ITEM IS NULL");
            return false;
        }
        if(first.count == first.item.stackLimit)
        {
            Debug.Log(first.count + ",,,," + first.item.stackLimit);
            Debug.Log("NO SPACE IN STACK");
            return false;
        }
        if(!first.item.GetType().Equals(second.GetType()))
        {
            Debug.Log("NOT EQUAL");
            return false;
        }
        return true;
    }
}
