using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warrior : menuButtonScript
{
    public GameObject menu;
    public override void Run()
    {
        GameObject accept = GameObject.Find("Accept");
        accept.GetComponent<CreateCharacter>().job = gameObject.GetComponentInChildren<Text>().text;
        accept.GetComponent<CreateCharacter>().Create();
    }
}
