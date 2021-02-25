using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifespan;
    public Player parent;
    void Start()
    {
        
    }

    void Update()
    {
        lifespan -= Time.deltaTime;
        if(lifespan <= 0)
        {
            parent.isAttacking = false;
            Object.Destroy(gameObject);
        }
    }
}
