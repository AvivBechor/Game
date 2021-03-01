using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private Player player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("moving", player.isMoving);
        switch (player.rotation)
        {
            case Side.UP:
                animator.SetFloat("moveY", 1);
                animator.SetFloat("moveX", 0);
                break;
            case Side.DOWN:
                animator.SetFloat("moveY", -1);
                animator.SetFloat("moveX", 0);
                break;
            case Side.LEFT:
                animator.SetFloat("moveX", -1);
                animator.SetFloat("moveY", 0);
                break;
            case Side.RIGHT:
                animator.SetFloat("moveX", 1);
                animator.SetFloat("moveY", 0);
                break;
        }
    }
}
