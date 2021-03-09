using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableItem : Item
{
    public UsableItem(string name, int id, Sprite sprite, int stackLimit, string description) : base(name, id, sprite, stackLimit, description)
    {

    }
    public abstract void Use();
}
