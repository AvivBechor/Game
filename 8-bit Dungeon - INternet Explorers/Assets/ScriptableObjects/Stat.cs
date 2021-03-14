using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Stat
    {
        public int value;
        public float statMultiplayer;
        public int mod;
        public Stat(int value, float statMultiplayer)
        {
            this.value = value;
            this.statMultiplayer = statMultiplayer;
        }
        public abstract void levelUp();
    }
}
