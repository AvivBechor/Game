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
        
        public MaxHp(string title)
        {
            value = 100;
            statMultiplayer = 0; //read from file classes.txt and take statmult of var title; this will not be 0 this will be the value from the file that tells the stat multiplayer by player class.
        }
        public override void levelUp()
        {
            throw new NotImplementedException(); // some algorithm to determine how much u get per level depending on your level and title.
        }
    }
}
