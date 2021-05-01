using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;
    private const int LEFT_CLICK = 0;
    public AttackHolder attack;
    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {

    }
}
