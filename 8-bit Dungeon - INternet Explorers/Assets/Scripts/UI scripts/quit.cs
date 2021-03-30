using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quit : menuButtonScript
{
    public override void Run()
    {
        Application.Quit();
    }
}
