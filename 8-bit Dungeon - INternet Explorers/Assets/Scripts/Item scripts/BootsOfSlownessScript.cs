using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsOfSlownessScript : Equipment
{
    public void Start()
    {
        stats.Add("Defense", 5);
        stats.Add("Speed", -2);
    }
}
