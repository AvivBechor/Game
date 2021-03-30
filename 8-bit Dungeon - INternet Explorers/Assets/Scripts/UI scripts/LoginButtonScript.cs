using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginButtonScript : menuButtonScript
{
    public override void Run()
    {
        SceneManager.LoadScene("Character creator");
    }
}
