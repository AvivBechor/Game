using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Stat:ScriptableObject
    {
        public int value;
        public int statMultiplayer;
        public abstract void levelUp();
    }
}
