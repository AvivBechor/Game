using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public Item test;


    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown("a"))
        {
            Debug.Log("CLICK");
            AddItem(test);
        }
    }

    public int AddItem(Item item)
    {
        if (item != null) {
            //Look for an inventory slot that can contain the item
            var res = this
                    .GetComponentsInChildren<ItemContainer>()
                    .FirstOrDefault(i => CanContain(i, item));           
            //If we found one
            if (res)
            {
                //If the slot is empty, set its item and set its sprite
                if (res.item == null)
                {
                    res.item = item;
                    res.SetSprite();
                }
                //In either case, add one to its count
                res.count++;
            }
            //If we didn't find a slot, the inventory is full
            else
            {
                return 0;
            }
            return 1;
        }
        return 0;
    }

    public int RemoveItem(Item item)
    {
        var res = this
                    .GetComponentsInChildren<ItemContainer>()
                    .FirstOrDefault(i => CanContain(i, item));
        if(res)
        {
            res.item = null;
            return 1;
        }
        return 0;
    }

    public bool CanContain(ItemContainer container, Item item)
    {
        //If the slot has no item, it can fit 
        if(container.item == null)
        {
            return true;
        }
        //If the slot is full, ignore it
        if(container.count == container.item.stackLimit)
        {
            return false;
        }
        //If the slot has an item of a different type, ignore it
        if(!container.item.GetType().Equals(item.GetType()))
        {
            return false;
        }
        //If the slot contains an item of the same type and is not full, it can fit
        return true;
    }
}
