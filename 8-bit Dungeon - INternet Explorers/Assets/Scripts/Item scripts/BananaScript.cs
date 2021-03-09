using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaScript : UsableItem
{
    public BananaScript(string name, int id, Sprite sprite, int stackLimit, string description) : base(name, id, sprite, stackLimit, description)
    {
        //base
    }

    override
    public void Use()
    {
        Debug.Log("Ate " + name);

    }
}
