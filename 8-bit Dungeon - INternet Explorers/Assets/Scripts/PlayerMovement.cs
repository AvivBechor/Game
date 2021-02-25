using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    public Rigidbody2D myRigidbody;
    public BoxCollider2D playerCollider;
    public LayerMask obstacle;
    Animator animator;
    Vector2 change;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        change = Vector2.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (change != Vector2.zero)
        {
            player.isMoving = true;
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
        }
        else
        {
            player.isMoving = false;
        }
        animator.SetBool("moving", player.isMoving);
    }

    void MoveCharacter()
    {
        Vector3 diff = change.normalized * player.moveSpeed * Time.deltaTime;
        myRigidbody.MovePosition(transform.position + diff);
    }
}
