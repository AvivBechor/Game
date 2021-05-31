using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Implode : MonoBehaviour
{
    public float lifespan;
    private float accum;

    void Update()
    {
        if (lifespan > 0)
        {
            accum += Time.deltaTime;
            if (accum >= lifespan)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
