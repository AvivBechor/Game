using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 5f;
    public LayerMask collidable;
    public LayerMask Trap;
    public Transform movePoint;
    public SpriteRenderer spriteRenderer;
    public Sprite UpSprite;
    public Sprite DownSprite;
    public Sprite LeftSprite;
    public Sprite RightSprite;
    public Side rotation = Side.DOWN;
    public bool isMoving = false;
    public bool dead = false;

    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(transform.position, movePoint.position) <= 0.05f)) {
            isMoving = false;
        } else
        {
            isMoving = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, movementSpeed * Time.deltaTime);

        if(rotation == Side.UP)
        {
            this.spriteRenderer.sprite = UpSprite;
        }
        else if (rotation == Side.DOWN)
        {
            this.spriteRenderer.sprite = DownSprite;
        }
        else if (rotation == Side.RIGHT)
        {
            this.spriteRenderer.sprite = RightSprite;
        }
        if (rotation == Side.LEFT)
        {
            this.spriteRenderer.sprite = LeftSprite;
        }
        if (!isMoving && !Physics2D.OverlapCircle(transform.position, 0.2f, Trap) && dead == false)
        {
            if (Vector3.Distance(transform.position, movePoint.position) == 0)
            {
                if (Input.GetKey("up"))
                {
                    rotation = Side.UP;
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), .2f, collidable))
                    {
                        movePoint.position += new Vector3(0f, 1f, 0f);
                    }
                }
                else if (Input.GetKey("down"))
                {
                    rotation = Side.DOWN;
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .2f, collidable))
                    {
                        movePoint.position += new Vector3(0f, -1f, 0f);
                    }
                }
                else if (Input.GetKey("right"))
                {
                    rotation = Side.RIGHT;
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f, 0f), .2f, collidable))
                    {
                        movePoint.position += new Vector3(1f, 0f, 0f);
                    }
                }
                else if (Input.GetKey("left"))
                {
                    rotation = Side.LEFT;
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f, 0f), .2f, collidable))
                    {
                        movePoint.position += new Vector3(-1f, 0f, 0f);
                    }
                }
            }
        }
    }
}
