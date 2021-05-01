using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu]
    public class MaxHp : Stat
    {
        public MaxHp(int value, float statMultiplayer) : base(value, statMultiplayer) { }
        public override void levelUp()
        {
            value = value + statMultiplayer;
        }
    }
}
