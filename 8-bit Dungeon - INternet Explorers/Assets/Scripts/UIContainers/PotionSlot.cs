using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSlot : EquipmentSlot
{
    public new void Start()
    {
        canContain = typeof(Potion);
    }
}
