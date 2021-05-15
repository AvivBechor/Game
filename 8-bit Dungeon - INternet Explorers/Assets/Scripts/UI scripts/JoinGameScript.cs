using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoinGameScript : menuButtonScript
{
    public override void Run()
    {
        SceneManager.LoadScene("LoadingScreen");
        GameObject.Find("Player").GetComponent<Player>().isInteracting = false;
    }
}
