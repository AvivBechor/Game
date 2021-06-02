using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerTrack : MonoBehaviour
{
    private GameObject player;
    public float max_x;
    public float max_y;
    public float min_y;
    public float min_x;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = player.transform.position;
        newPosition.x = Mathf.Min(max_x, Mathf.Max(min_x, player.transform.position.x));
        newPosition.y = Mathf.Min(max_y, Mathf.Max(min_y, player.transform.position.y));
        newPosition.z = gameObject.transform.position.z;
        gameObject.transform.position = newPosition;
    }
}
