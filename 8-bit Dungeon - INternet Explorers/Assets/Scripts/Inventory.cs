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
            //Look for inventory slot containing an item of the same type as the input that is not full
            var res = inventoryPanel
                    .GetComponentsInChildren<ItemContainerScript>()
                    .FirstOrDefault(i => findAvailableSlot(i, item));

            Debug.Log("res" + res);
            //If we found and object, add one to the count
            if (res)
            {
                res.count++;
            }
            //If we didn't
            else
            {
                //look for the earliest empty slot
                var emptySlot = inventoryPanel.GetComponentsInChildren<ItemContainerScript>()
                    .Where(i => i.item == null)
                    .FirstOrDefault();
                //If we find it, set its item to the input and set the count to one
                if(emptySlot)
                {
                    emptySlot.count = 1;
                    emptySlot.item = item;
                }
                //If we didn't declane that there's no place for the item and the inventory is full
                else
                {
                    Debug.Log("Inventory is full");
                }
            }
        }
    }

    public bool findAvailableSlot(ItemContainerScript container, Item item)
    {
        //If the slot has no item, ignore it
        if(container.item == null)
        {
            //Debug.Log("ITEM IS NULL");
            return false;
        }
        //If the slot is full, ignore it
        if(container.count == container.item.stackLimit)
        {
            //Debug.Log(container.count + ",,,," + container.item.stackLimit);
            //Debug.Log("NO SPACE IN STACK");
            return false;
        }
        //If the slot has an item of a different type, ignore it
        if(!container.item.GetType().Equals(item.GetType()))
        {
            //Debug.Log("NOT EQUAL");
            return false;
        }
        return true;
    }
}
