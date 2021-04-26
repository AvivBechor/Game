using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class CreateCharacter : MonoBehaviour
{
    public Dropdown job;
    public Button self;
    public Character character;
    public IntStorage currentHp;
    public IntStorage currentRecourse;

    // Start is called before the first frame update
    void Start()
    {
        self.onClick.AddListener(Create);
        Character a = ScriptableObject.CreateInstance<Character>();
    }

    void Create()
    {
        if (job.value == 0)
        {
            character.title = "Warrior";
        }
        else if (job.value == 1)
        {
            character.title = "Mage";
        }
        else if (job.value == 2)
        {
            character.title = "Frog";
        }
        character.init();
        currentHp.value = character.stats["MaxHP"].value;
        currentRecourse.value = character.stats["MaxRecourse"].value;
        SceneManager.LoadScene("CitySample");
    }
}
