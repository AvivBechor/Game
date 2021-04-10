using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
    private Player player;
    public GameObject playerInteractor;
    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("x") && !player.isAttacking && !player.isInteracting)
        {
            playerInteractor.transform.position = player.transform.position + player.getRotationVector();
        }
    }
}
