using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Enemy : MonoBehaviour
{
    public string name;
    public Dictionary<string, Stat> stats;
    public int level;
    public int movementSpeed;
    public Side rotation;
}
