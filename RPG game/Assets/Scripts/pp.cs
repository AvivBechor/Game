using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class pp : MonoBehaviour
{
    public VectorValue startingPosition;
    public Transform movePoint;
    public Side rotation = Side.DOWN;
    public const int MAXHP = 100;
    public IntStorage HP;
    public bool isDead;
    public bool isMoving;
    public bool isInteracting;
    public bool isAttacking;
    public bool isVunerable;

    private void Awake()
    {
        HP.value = MAXHP;
    }
    private void Start()
    {
        transform.position = startingPosition.initialValue;
        movePoint.transform.position = transform.position;
        movePoint.parent = null;
    }

    private void Update()
    {
        if(HP.value <= 0)
        {
            isDead = true;
            if (Input.GetKeyDown("space"))
            {
                isDead = false;
                HP.value = MAXHP;
            }
        }
    }

}
