using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsSlot : EquipmentSlot
{
    public new void Start()
    {
        canContain = typeof(Boots);
    }
}
