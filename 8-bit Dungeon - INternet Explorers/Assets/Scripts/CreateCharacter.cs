using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateCharacter : MonoBehaviour
{
    public Dropdown job;
    public Button self;
    public Character character;
    public IntStorage currentHp;

    // Start is called before the first frame update
    void Start()
    {
        self.onClick.AddListener(Create);
    }

    void Create()
    {
        if (job.value == 0)
        {
            character.maxHp.value = 300;
            character.title = "Warrior";
        }
        else if (job.value == 1)
        {
            character.maxHp.value = 150;
            character.title = "Mage";
        }
        else if (job.value == 2)
        {
            character.maxHp.value = 1;
            character.title = "Frog";
        }
        currentHp.value = character.maxHp.value;
        Debug.Log("You are a " + character.title + " with " + character.maxHp.value + " HP.");
        SceneManager.LoadScene("SampleScene");
    }
}
