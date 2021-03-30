using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelection : MonoBehaviour
{
    private List<Image> buttons = new List<Image>();
    private int index = 0;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;
    public KeyCode advance;
    public KeyCode regress;
    void Start()
    {
        foreach(Transform child in transform)
        {
            Image childImage = child.GetComponent<Image>();
           if (childImage) { buttons.Add(childImage); }
        }
        buttons[index].sprite = selectedSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(advance))
        {
            buttons[index].sprite = notSelectedSprite;
            index = index == 0 ? buttons.Count - 1 : index - 1;
            buttons[index].sprite = selectedSprite;
        }
        else if(Input.GetKeyDown(regress))
        {
            buttons[index].sprite = notSelectedSprite;
            //fuck this
            index = index == buttons.Count-1 ? 0 : index + 1;
            buttons[index].sprite = selectedSprite;
        }
        else if(Input.GetKeyDown("return"))
        {
            Debug.Log("IN");
            Debug.Log(buttons[index].GetComponent<menuButtonScript>());
            buttons[index].GetComponent<menuButtonScript>().Run();
        }
    }
}
