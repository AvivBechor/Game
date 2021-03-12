using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;


public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool isDragging = false;
    public Vector3 pos;

 
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging)
        {
            pos = transform.position;
            isDragging = true;
            //Debug.Log(current.container.item);
        }
        transform.position = Input.mousePosition;
        
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        transform.position = pos;
        RaycastHit2D[] allhits = Physics2D.RaycastAll(Input.mousePosition, -Vector2.up);
        if(allhits.Length > 1)
        {
            Item tempType = this.gameObject.GetComponent<ItemContainerScript>().item;
            int tempCount = this.gameObject.GetComponent<ItemContainerScript>().count;
            this.gameObject.GetComponent<ItemContainerScript>().count = allhits[1].collider.gameObject.GetComponent<ItemContainerScript>().count;
            this.gameObject.GetComponent<ItemContainerScript>().item = allhits[1].collider.gameObject.GetComponent<ItemContainerScript>().item;
            allhits[1].collider.gameObject.GetComponent<ItemContainerScript>().count = tempCount;
            allhits[1].collider.gameObject.GetComponent<ItemContainerScript>().item = tempType;
            allhits[1].collider.gameObject.GetComponent<ItemContainerScript>().SetSprite();
            this.gameObject.GetComponent<ItemContainerScript>().SetSprite();
        }
       
    }





}
