using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment : UseableItem
{
    public Dictionary<string, int> stats = new Dictionary<string, int>();
    public bool isEquipped;

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
    

}


