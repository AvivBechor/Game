using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Stat
    {
        public float value;
        public float statMultiplayer;
        public int mod;
        public Stat(int value, float statMultiplayer)
        {
            this.value = value;
            this.statMultiplayer = statMultiplayer;
        }
        public virtual void levelUp() {
            value = value * (1 + statMultiplayer);
        }
    }
}
