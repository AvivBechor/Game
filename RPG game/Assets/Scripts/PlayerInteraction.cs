using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PlayerInteraction : MonoBehaviour
{
    public bool isInteracting = false;
    public LayerMask Interactable;
    public GameObject Player;
    public Transform InteractPoint;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = Player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!isInteracting)
        {
            if (Input.GetKeyDown("x"))
            {
                Side rotation = playerMovement.rotation;
                if (rotation == Side.UP)
                {
                    Collider2D collider = Physics2D.OverlapCircle(transform.position + new Vector3(0f, 1f, 0f), 0.2f, Interactable);
                    Debug.Log(collider);
                    if (collider != null)
                    {
                        isInteracting = true;
                        collider.GetComponent<DialogueTrigger>().Interact();
                    }

                }
                else if (rotation == Side.DOWN)
                {
                    if (Physics2D.OverlapCircle(transform.position + new Vector3(0f, 1f, 0f), 0.2f, Interactable))
                    {

                    }
                }
                else if (rotation == Side.LEFT)
                {
                    if (Physics2D.OverlapCircle(transform.position + new Vector3(-1f, 0f, 0f), 0.2f, Interactable))
                    {

                    }
                }
                else if (rotation == Side.RIGHT)
                {
                    if (Physics2D.OverlapCircle(transform.position + new Vector3(1f, 0f, 0f), 0.2f, Interactable))
                    {

                    }
                }
            }
        }
    }
}
