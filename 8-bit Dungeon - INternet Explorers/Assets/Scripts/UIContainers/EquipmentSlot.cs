using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemContainer
{
    public new void Start()
    {
        canContain = typeof(Equipment);
    }
}
