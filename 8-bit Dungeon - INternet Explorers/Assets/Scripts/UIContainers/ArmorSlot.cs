using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSlot : EquipmentSlot
{
    public new void Start()
    {
        canContain = typeof(Armor);
    }
}
