using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
   public class DialogueTrigger : Interactable
    {
        public Dialogue dialogue;
        public override void Interact()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(this);
            isInteracting = true;
        }


        void Update()
        {
            if(isInteracting)
            {
                if(Input.GetKeyDown("x")) {
                    FindObjectOfType<DialogueManager>().DisplayNextSentence(this);
                }
            }
        }
    }
}
