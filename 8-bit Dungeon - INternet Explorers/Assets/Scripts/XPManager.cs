using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPManager
{
    public XP classXP;
    public ProXP spellXP;
    public ProXP combatXP;
    public ProXP agilityXP;


    public XPManager(int[] classXPLevels, int[] spellXPLevels, int[] combatXPLevels, int[] agilityXPLevels)
    {
        classXP   =  new XP(classXPLevels);
        spellXP   =  new ProXP(spellXPLevels);
        combatXP  =  new ProXP(combatXPLevels);
        agilityXP =  new ProXP(agilityXPLevels);
    }

    public int addPoint(ProXP xp)
    {
        if(xp.points == 3)      
            return -1;
        
        if(spellXP.points + combatXP.points + agilityXP.points == 6)       
            return -1;

        xp.points++;
        return 0;
    }
    
    public int getClassXPPoints()
    {
        return (int)(classXP.level / 5) + 1;
    }
}
