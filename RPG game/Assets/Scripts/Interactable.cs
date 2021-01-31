using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Interactable:MonoBehaviour
    {
        public string Name;
        public bool isInteractable;
        public bool isInteracting = false;

        public abstract void Interact();
    }
}