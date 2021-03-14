using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    public Item item;
    public int count;
    public System.Type canContain;

    public void Start()
    {
        canContain = typeof(Item);
        SetSprite();
    }
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

    public bool isEmpty()
    {
        return item == null;
    }

}
