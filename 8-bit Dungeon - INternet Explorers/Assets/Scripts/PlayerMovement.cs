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
        if (!player.isAttacking && !player.isDead)
        {
            change = Vector2.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            if (change != Vector2.zero)
            {
                player.isMoving = true;
                MoveCharacter();
            }
            else
            {
                player.isMoving = false;
            }
            if (change.x == 1)
            {
                player.rotation = Side.RIGHT;
                animator.SetFloat("moveX", 1);
                animator.SetFloat("moveY", 0);
            }
            else if (change.x == -1)
            {
                player.rotation = Side.LEFT;
                animator.SetFloat("moveX", -1);
                animator.SetFloat("moveY", 0);
            }
            else if (change.y == 1)
            {
                player.rotation = Side.UP;
                animator.SetFloat("moveY", 1);
                animator.SetFloat("moveX", 0);
            }
            else
            {
                player.rotation = Side.DOWN;
                animator.SetFloat("moveY", -1);
                animator.SetFloat("moveX", 0);
            }
            animator.SetBool("moving", player.isMoving);
        }
    }

    void MoveCharacter()
    {
        Vector3 diff = change.normalized * player.moveSpeed * Time.deltaTime;
        myRigidbody.MovePosition(transform.position + diff);
    }
}
