using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemContainerScript : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public int count;

    public void SetSprite()
    {
        if(item)
        {
            GetComponent<Image>().sprite = item.sprite;
        } 
        else
        {
            GetComponent<Image>().sprite = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int tap = eventData.clickCount;

        if(item is UseableItem && tap == 2)
        {
            ((UseableItem)item).Use();
            if(--count == 0)
            {
                item = null;
            }
        }
       
    }
}
