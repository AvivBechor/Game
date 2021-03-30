using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuitButtonScript : menuButtonScript
{
    public GameObject confirmationPanel;
    public GameObject menu;
    public override void Run()
    {
        confirmationPanel.SetActive(true);
        menu.SetActive(false);

    }
}
