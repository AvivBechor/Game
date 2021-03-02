using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifespan;
    public Player parent;
    public int speed;
    public float disableDuration;
    private Vector3 direction;

    private void Start()
    {
        direction = parent.getRotationVector();
    }
    void Update()
    {
        move();
        lifespan -= Time.deltaTime;
        disableDuration -= Time.deltaTime;
        if(disableDuration <= 0)
        {
            parent.isAttacking = false;
        }
        if(lifespan <= 0)
        {
            kill();
        }
    }

    public void kill()
    {
        Object.Destroy(gameObject);
    }

    void move()
    {
        Vector3 step = speed * direction * Time.deltaTime;
        Debug.Log(step);
        transform.position += step;
    }
}
