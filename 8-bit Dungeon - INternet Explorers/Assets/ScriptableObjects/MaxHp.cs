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
        public override void levelUp()
        {
            throw new NotImplementedException(); // some algorithm to determine how much u get per level depending on your level and title.
        }
    }
}
