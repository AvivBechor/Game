using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    private const int LEFT_CLICK = 0;
    public AttackHolder attack;
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(LEFT_CLICK) && !player.isAttacking)
        {
            player.isAttacking = true;
            if (player.rotation == Side.UP)
            {
                Object.Instantiate(attack.attack, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
            }
            else if (player.rotation == Side.DOWN)
            {
                Object.Instantiate(attack.attack, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.rotation);
            }
            else if (player.rotation == Side.RIGHT)
            {
                Object.Instantiate(attack.attack, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), transform.rotation);
            }
            else
            {
                Object.Instantiate(attack.attack, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), transform.rotation);
            }

        }
    }
}
