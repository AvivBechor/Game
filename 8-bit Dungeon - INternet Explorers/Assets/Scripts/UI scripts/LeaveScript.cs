using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveScript : menuButtonScript
{

    public override void Run()
    {
        GameObject.Find("Player").GetComponent<Player>().isInteracting = false;
        GameObject.Find("Panel").SetActive(false);
        GameObject player = GameObject.Find("Player");
        player.transform.position = new Vector3(25, 16, player.transform.position.z);
    }
}
