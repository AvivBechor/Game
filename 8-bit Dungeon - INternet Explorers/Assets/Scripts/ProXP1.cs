using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProXP : XP
{
    public int points;

    public ProXP(int[] levelCaps) : base(levelCaps)
    {
        points = 0;
    }
}
