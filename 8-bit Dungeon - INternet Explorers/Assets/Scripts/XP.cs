using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP
{

    public int experience { get; protected set; }
    public int level { get; protected set; }
    protected readonly int[] levelCaps;
    public bool locked;

    public XP(int[] levelCaps)
    {
        this.levelCaps = levelCaps;
        this.level = 1;
        this.experience = 0;
        this.locked = false;
    }

   

    public void addXP(int addition)
    {
        if (!locked)
        {
            int newXP = experience + addition;
            if (newXP > levelCaps[level - 1])
            {
                newXP = levelCaps[level - 1];
            }
            experience = newXP;
        }
    }

    public virtual int levelUp()
    {
        if (!locked)
        {
            if (experience == levelCaps[level - 1])
            {
                level++;
                return 0;
            }
        }
        return -1;
    }

}
