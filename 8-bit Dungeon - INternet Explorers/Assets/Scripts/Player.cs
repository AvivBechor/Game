using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Player : MonoBehaviour
{
    public bool inGame;
    public bool singlePlayer;
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
    public bool gender;
    
    public bool canMove()
    {
        if(!singlePlayer)
        {
            return !isAttacking && !isDead && !isInteracting && inGame;
        }
        return !isAttacking && !isDead && !isInteracting;
    }

    public float getModStatValue(string name)
    {
        foreach (KeyValuePair<string, Stat> entry in character.stats)
        {
            Debug.Log("KEY :"+entry.Key + "VAL:" + entry.Value.value);
            Debug.Log(entry.Key.Equals(name));
        }
        return character.stats[name].value + character.stats[name].mod;
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
