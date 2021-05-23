using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class enemyMovementScript : MonoBehaviour
{
    public Enemy enemy;
    public float xMovement;
    public float yMovement;
    public Rigidbody2D myRigidbody;
    //public BoxCollider2D playerCollider;
    public Vector2 change;
    public void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        //X and Y movement to be set by the server
        change = Vector2.zero;
        change.x = xMovement;
        change.y = yMovement;

        //Sets the player's rotation value based on the input.
        setRotation(change);
        //Sets the player's moving value based on the input
        //setMoving(change);
        //Moves the character based on the input.           
        moveCharacter(change);
        switch(enemy.rotation)
        {
            case Side.UP:
                gameObject.GetComponent<SpriteRenderer>().sprite = enemy.spriteUP;
                break;
            case Side.DOWN:
                gameObject.GetComponent<SpriteRenderer>().sprite = enemy.spriteDOWN;
                break;
            case Side.LEFT:
                gameObject.GetComponent<SpriteRenderer>().sprite = enemy.spriteLEFT;
                break;
            case Side.RIGHT:
                gameObject.GetComponent<SpriteRenderer>().sprite = enemy.spriteRIGHT;
                break;
        }

    }

    void moveCharacter(Vector2 change)
    {
        //Create a new vector that is the distance the player moves within Time.deltaTime
        Vector3 step = change.normalized * enemy.stats["Movespeed"].value * Time.deltaTime;
        //Adds the step to the current position.
        myRigidbody.MovePosition(transform.position + step);
    }

    void setRotation(Vector2 change)
    {
        //Checks if there is movement in X. X and Y can only be -1, 0, and 1.
        if (change.x != 0)
        {
            //Checks if the movement is right (1) or left (-1), and sets the rotation value accordingly.
            if (change.x == 1)
            {
                enemy.rotation = Side.RIGHT;
            }
            else
            {
                enemy.rotation = Side.LEFT;
            }
        }
        //In the case that X is zero, meaning that there is no movement in X, and that there is movement in Y.
        else if (change.y != 0)
        {
            //Checks if the movement is up (1) or down (-1) and sets the rotation value accordingly.
            if (change.y == 1)
            {
                enemy.rotation = Side.UP;
            }
            else
            {
                enemy.rotation = Side.DOWN;
            }
        }
    }

}


