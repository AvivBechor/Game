using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlot : EquipmentSlot
{
    public new void Start()
    {
        canContain = typeof(Hand);
    }
}
