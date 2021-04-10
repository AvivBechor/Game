using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorScript : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Iinteractable h = collision.GetComponent<Iinteractable>();
        if (h != null)
        {
            player.isInteracting = true;
            h.run(player);
        }
    }
}
