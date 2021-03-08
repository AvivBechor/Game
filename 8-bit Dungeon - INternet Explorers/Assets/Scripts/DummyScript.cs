using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP;
    public GameObject serverHandler;
    private Client client;
    public int movx, movy;
    void Start()
    {
        client = serverHandler.GetComponent<Client>();
        HP = 100;
        movx = 0;
        movy = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Attack a = collision.gameObject.GetComponent<Attack>();
        HP--;
    }
    private void Update()
    {
        if (client.instructions.Count > 0)
        {

            if (client.instructions.Peek().Split(':')[0] == "e")
            {
                string data = client.instructions.Dequeue();
                string[] pos = data.Substring(2).Split(',');
                movx = int.Parse(pos[0]);
                movy = int.Parse(pos[1]);
                transform.position += new Vector3(movx, movy, 0) * Time.deltaTime;
            }
            else
            {
                client.instructions.Dequeue();
            }
        }
        
    }
    
}
