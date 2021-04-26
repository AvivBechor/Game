using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MakeWallTransparent : MonoBehaviour
{
    public Tilemap wall;
    public Tilemap window;
    public LayerMask walllm;
    public GameObject player;
    public void Update()
    {
        if(Physics2D.OverlapCircle(player.transform.position, .2f, walllm))
        {
            if (wall.color.a > 0.1)
                wall.color = new Color(wall.color.r, wall.color.g, wall.color.b, wall.color.a - 0.05f);
            if(window.color.a > 0.1)
                window.color = new Color(window.color.r, window.color.g, window.color.b, window.color.a - 0.05f);
            
        } 
        else
        {
            if (wall.color.a < 1)
                wall.color = new Color(wall.color.r, wall.color.g, wall.color.b, wall.color.a + 0.05f);
            if (window.color.a < 1)
                window.color = new Color(window.color.r, window.color.g, window.color.b, window.color.a + 0.05f);
        }
    }
}
