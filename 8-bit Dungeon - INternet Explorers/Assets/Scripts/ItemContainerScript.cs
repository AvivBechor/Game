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
        GetComponent<Image>().sprite = item.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int tap = eventData.clickCount;

        if(item is UsableItem && tap == 2)
        {
            ((UsableItem)item).Use();
        }
    }
}
