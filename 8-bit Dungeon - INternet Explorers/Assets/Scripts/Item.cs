using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public new string name;
    public string description;
    public int id;
    public Sprite sprite;
    public int stackLimit;

    public Item(string name, int id, Sprite sprite, int stackLimit, string description) 
    {
        this.name = name;
        this.id = id;
        this.sprite = sprite;
        this.stackLimit = stackLimit;
        this.description = description;
    }
}
