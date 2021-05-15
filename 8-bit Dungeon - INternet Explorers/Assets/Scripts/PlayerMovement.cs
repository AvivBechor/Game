using Assets.Scripts;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player player;
    public float xMovement;
    public float yMovement;
    public Rigidbody2D myRigidbody;
    public BoxCollider2D playerCollider;
    public Vector2 change;
    public SendQueue sendQueue;
    private int playerUUID;
    private int gameID;
    private Vector2 previousChange;
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        previousChange = Vector2.zero;
        Debug.Log("PKLAYER IS" + player);

        if (!player.singlePlayer)
        {
            //sendQueue = GameObject.Find("Connection").GetComponent<SendQueue>();
            playerUUID = player.GetComponent<UUIDHandler>().UUID;
            Debug.Log("playr is" + player);
            Debug.Log("player ID is " + playerUUID);
            gameID = player.GetComponent<gameIDHandler>().gameID;
        }
    }

    void FixedUpdate()
    {
        if (player.canMove())
        {
            //Variables to control player movement and rotation, will later be used by the server as well and not only the keyboard input.
            //xMovement = 0;
            //yMovement = 0;  
            xMovement = Input.GetAxisRaw("Horizontal");
            yMovement = Input.GetAxisRaw("Vertical");

            change = Vector2.zero;
            change.x = xMovement;
            change.y = yMovement;

            //Sets the player's rotation value based on the input.

            setRotation(change);
            //Sets the player's moving value based on the input

            setMoving(change);
            //Moves the character based on the input.           
            moveCharacter(change);

            if (player.inGame)
            {
                Debug.Log("PALYER IS IN GAME;");
                if (!change.Equals(previousChange))
                {
                    Debug.Log("THEY ARE NOT EUQLA WER ARE IN :)");
                    if (sendQueue)
                    {
                        Debug.Log("queue exists");
                        sendQueue.addMessage("mov:" + gameID + ":" + playerUUID + ":" + change.x + "," + change.y);
                        previousChange = change;
                    }
                    else
                        Debug.Log("queue doesnt exist");
                }

            }
        }
    }

    void moveCharacter(Vector2 change)
    {
        //Create a new vector that is the distance the player moves within Time.deltaTime
        Vector3 step = change.normalized * player.moveSpeed * Time.deltaTime;
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
                player.rotation = Side.RIGHT;
            }
            else
            {
                player.rotation = Side.LEFT;
            }
        }
        //In the case that X is zero, meaning that there is no movement in X, and that there is movement in Y.
        else if (change.y != 0)
        {
            //Checks if the movement is up (1) or down (-1) and sets the rotation value accordingly.
            if (change.y == 1)
            {
                player.rotation = Side.UP;
            }
            else
            {
                player.rotation = Side.DOWN;
            }
        }
    }

    void setMoving(Vector2 change)
    {
        //Checks if there is any movement, meaning if the movement vector is not equal to (0, 0), and sets the isMoving value accordingly.
        if (change != Vector2.zero)
        {
            player.isMoving = true;
        }
        else
        {
            player.isMoving = false;
        }
    }
}
