using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class CreateCharacter : MonoBehaviour
{
    public string job;
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

    public void Create()
    {
        character.title = job;
        
        character.init();
        currentHp.value = (int)character.stats["MaxHP"].value;
        currentRecourse.value = (int)character.stats["MaxRecourse"].value;
        SceneManager.LoadScene("Forest");
    }
}
