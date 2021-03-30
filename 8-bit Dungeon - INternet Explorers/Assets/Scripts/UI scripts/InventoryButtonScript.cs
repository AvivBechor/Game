using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtonScript : menuButtonScript
{
    public GameObject inventory;
    public override void Run()
    {
        GameObject menu = GameObject.Find("Menu");
        inventory.SetActive(true);
        menu.SetActive(false);
    }
}
