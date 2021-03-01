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
            //player.isAttacking = true;            

        }
    }
}
