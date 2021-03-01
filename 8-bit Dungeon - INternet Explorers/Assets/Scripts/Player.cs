﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Player : MonoBehaviour
{
    public VectorValue startingPosition;
    public IntStorage CurrentHP;
    public Side rotation = Side.DOWN;
    public Character character;
    public int moveSpeed;
    public bool isDead;
    public bool isMoving;
    public bool isInteracting;
    public bool isAttacking;
    public bool isVunerable;
    private Vector3 change;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if(CurrentHP.value <= 0)
        {
            isDead = true;
            if (Input.GetKeyDown("space"))
            {
                isDead = false;
                CurrentHP.value = character.maxHp.value;
            }
        }        
    }   
}
