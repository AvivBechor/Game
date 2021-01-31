using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class DialogueManager : MonoBehaviour
{
    public GameObject Player;
    private Queue<string> sentences;
    
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(DialogueTrigger dialogueTrigger)
    {
        Debug.Log("Starting conversation with " + dialogueTrigger.dialogue.title);

        sentences.Clear();

        foreach(string sentence in dialogueTrigger.dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(dialogueTrigger);
    }
    public void DisplayNextSentence(DialogueTrigger dialogueTrigger)
    {
        if(sentences.Count == 0)
        {
            EndDialogue(dialogueTrigger);
            
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
    }

    void EndDialogue(DialogueTrigger dialogueTrigger)
    {
        Debug.Log("End of conversation.");
        Player.GetComponent<PlayerInteraction>().isInteracting = false;
        dialogueTrigger.isInteracting = false;
    }

}
