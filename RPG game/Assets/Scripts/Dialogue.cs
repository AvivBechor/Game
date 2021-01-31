using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public class Dialogue
    {
        public string title;
        [TextArea(3,10)]
        public string[] sentences;

    }
}
