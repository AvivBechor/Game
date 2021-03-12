using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaScript : UseableItem
{
    override
    public void Use()
    {
        Debug.Log("Ate " + name);

    }
}
