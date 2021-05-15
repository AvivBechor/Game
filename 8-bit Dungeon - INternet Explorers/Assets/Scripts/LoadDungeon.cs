using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDungeon : MonoBehaviour
{

    public GameObject panel;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        panel.SetActive(true);
        GameObject.Find("Player").GetComponent<Player>().isInteracting = true;
    }
}
