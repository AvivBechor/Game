using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Player : MonoBehaviour
{
    public VectorValue startingPosition;
    public IntStorage CurrentHP;
    public IntStorage CurrentRecource;
    public Side rotation = Side.DOWN;
    public Character character;
    public int moveSpeed;
    public bool isDead;
    public bool isMoving;
    public bool isInteracting;
    public bool isAttacking;
    public bool isVunerable;
    
    private void Start()
    {
        
    }

    private void Update()
    {
      
    }

    public bool canMove()
    {
        return !isAttacking && !isDead && !isInteracting;
    }

    public Vector3 getRotationVector()
    {
        Vector3 res = new Vector3(0, 0, 0);
        switch (rotation) {
            case Side.UP:
                res = new Vector3(0, 1, 0);
                break;
            case Side.DOWN:
                res = new Vector3(0, -1, 0);
                break;
            case Side.LEFT:
                res = new Vector3(-1, 0, 0);
                break;
            case Side.RIGHT:
                res = new Vector3(1, 0, 0);
                break;
        }
        return res;
    }
}
