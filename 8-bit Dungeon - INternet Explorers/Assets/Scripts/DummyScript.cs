using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP;
    void Start()
    {
        HP = 100;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Attack a = collision.gameObject.GetComponent<Attack>();
        HP--;
    }
}
