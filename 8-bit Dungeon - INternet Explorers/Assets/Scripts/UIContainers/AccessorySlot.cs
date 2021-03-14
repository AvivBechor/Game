using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessorySlot : EquipmentSlot
{
    public new void Start()
    {
        canContain = typeof(Accessory);
    }
}
