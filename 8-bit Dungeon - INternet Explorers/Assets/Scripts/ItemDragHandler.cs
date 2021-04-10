using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using System.Linq;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private bool isDragging = false;
    private Vector3 pos;
    public EquipmentManager equipmentManager;

    public void Start()
    {
        
        equipmentManager = gameObject.GetComponentInParent<EquipmentManager>();
        Debug.Log(equipmentManager);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("ELI"); 
        if (!isDragging)
        {
            if (GetComponent<ItemContainer>().item != null)
            {
                pos = transform.position;
                isDragging = true;
            }
        }
        if(isDragging)
        {
            transform.position = Input.mousePosition;
        }


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;
            ItemContainer thisItemContainer = this.gameObject.GetComponent<ItemContainer>();

            transform.position = pos;
            RaycastHit2D[] allhits = Physics2D.RaycastAll(Input.mousePosition, -Vector2.up);
            RaycastHit2D res = allhits
                                .Where((i) => !(thisItemContainer.Equals(i.collider.gameObject.GetComponent<ItemContainer>())))
                                .FirstOrDefault();
            if (res)
            {

                ItemContainer otherItemContainer = res.collider.gameObject.GetComponent<ItemContainer>();
                if (canSwap(thisItemContainer, otherItemContainer))
                {
                    Swap(thisItemContainer, otherItemContainer);
                    thisItemContainer.SetSprite();
                    otherItemContainer.SetSprite();                  
                }
            }
        }
    }
    private bool canSwap(ItemContainer container1, ItemContainer container2)
    {
        System.Type container1Type = container1.GetType();
        System.Type containerr2Type = container2.GetType();
        if(container1Type.Equals(containerr2Type))
        {
            return true;
        }
        if(container1Type.IsSubclassOf(typeof(EquipmentSlot)) && containerr2Type.IsSubclassOf(typeof(EquipmentSlot)))
        {
            return false;
        }
        ItemContainer actualItemContainer;
        ItemContainer otherItemContainer;
        if(container1Type.Equals(typeof(ItemContainer)))
        {
            actualItemContainer = container1;
            otherItemContainer = container2;
        } 
        else
        {
            actualItemContainer = container2;
            otherItemContainer = container1;
        }

        if (actualItemContainer.isEmpty() == true)
        {
            return true;
        }
        if(actualItemContainer.item.GetType().IsSubclassOf(otherItemContainer.canContain))
        {
            return true;
        }
        return false;

    }
    private void Swap(ItemContainer itemContainer1, ItemContainer itemContainer2)
    {
        Item item1 = itemContainer1.item;
        Item item2 = itemContainer2.item;
        itemContainer1.item = item2;
        itemContainer2.item = item1;
        int count1 = itemContainer1.count;
        int count2 = itemContainer2.count;
        itemContainer1.count = count2;
        itemContainer2.count = count1;
        bool isEquipment = itemContainer1.GetType().IsSubclassOf(typeof(EquipmentSlot)) || itemContainer2.GetType().IsSubclassOf(typeof(EquipmentSlot));
        if (isEquipment)
        {
            ItemContainer actualItemContainer;
            ItemContainer otherItemContainer;
            if (itemContainer1.GetType().Equals(typeof(ItemContainer)))
            {
                actualItemContainer = itemContainer1;
                otherItemContainer = itemContainer2;
            }
            else
            {
                actualItemContainer = itemContainer2;
                otherItemContainer = itemContainer1;
            }
            if (actualItemContainer.item)
            {
                ((Equipment)(actualItemContainer.item)).isEquipped = false;
            }
            if (otherItemContainer.item)
            {
                ((Equipment)(otherItemContainer.item)).isEquipped = true;
            }
            Debug.Log("em:" + equipmentManager + ", otheritem:" + otherItemContainer + ", actual" + actualItemContainer);
            equipmentManager.apply(otherItemContainer.item, actualItemContainer.item);
        }
        
    }





}
