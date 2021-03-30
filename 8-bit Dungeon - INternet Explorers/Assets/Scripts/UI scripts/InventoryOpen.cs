using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOpen : MonoBehaviour
{
    public GameObject inventory;
    private Player player;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            foreach (Transform child in transform)
            {
                if(child.gameObject.name == "Inventory") {
                    child.gameObject.SetActive(!child.gameObject.activeSelf);
                    player.isInteracting = child.gameObject.activeSelf;
                }
                else
                {
                    child.gameObject.SetActive(false);
                }             
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name == "Menu")
                {
                    child.gameObject.SetActive(!child.gameObject.activeSelf);
                    player.isInteracting = child.gameObject.activeSelf;
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
            
        }
    }
}
