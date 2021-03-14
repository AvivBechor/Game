using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOpen : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("i"))
        {
            
            pauseCanvas.SetActive(!pauseCanvas.activeSelf);
            player.GetComponent<Player>().isInteracting = pauseCanvas.activeSelf;
            player.GetComponent<Player>().isMoving = !pauseCanvas.activeSelf;
        }
    }
}
