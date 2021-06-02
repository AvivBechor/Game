using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class gotoforestbye : MonoBehaviour
{
    

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(onClick);
        
    }

    void onClick()
    {

        //  =====
        //=========
        //==========
        //  =====
        SceneManager.LoadScene("Forest");
        //  =====
        //==========
        //=========
        //  =====
    }
}

