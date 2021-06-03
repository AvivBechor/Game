using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = player.CurrentHP.value.ToString() + "/" + player.character.stats["MaxHP"].value.ToString();
    }
}
