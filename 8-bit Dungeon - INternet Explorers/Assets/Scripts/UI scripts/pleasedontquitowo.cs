using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pleasedontquitowo : menuButtonScript
{
    public GameObject menu;
    public override void Run()
    {
        menu.SetActive(true);
        GameObject.Find("Confirmation panel").SetActive(false);
    }
}
