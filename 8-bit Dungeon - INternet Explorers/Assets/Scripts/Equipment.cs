using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment : UseableItem
{
    public Dictionary<string, int> stats = new Dictionary<string, int>();
    public EquipmentManager equipmentManager;
    public Inventory inventory;
    public bool isEquipped;

    public override void Use()
    {
        throw new System.NotImplementedException();
    }

    private void itemToInventory(ItemContainerScript container)
    {
        if (inventory.AddItem(this) == 1)
        {
            container.item = null;
        }
        else
        {
            Debug.Log("NO SPACE IN INVENTORY");
        }
    }

    private void swapWithCurrentItem(ItemContainerScript container)
    {
        Item temp = this;
        inventory.RemoveItem(this);
        inventory.AddItem(container.item);
        container.item = temp;
    }
}


