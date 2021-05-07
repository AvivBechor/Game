using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPManager
{
    private XP classXP;
    private XP spellXP;
    private XP combatXP;
    private XP agilityXP;


    public XPManager(int[] classXPLevels, int[] spellXPLevels, int[] combatXPLevels, int[] agilityXPLevels)
    {
        classXP   =  new XP(classXPLevels);
        spellXP   =  new XP(spellXPLevels);
        combatXP  =  new XP(combatXPLevels);
        agilityXP =  new XP(agilityXPLevels);
    }

    private int addPoint(XP xp)
    {
        if(xp.level == 3)      
            return -1;
        
        if(spellXP.level + combatXP.level + agilityXP.level == 6)       
            return -1;

        return xp.levelUp();
    }

    public int addCombatPoint()
    {
        return addPoint(combatXP);
    }

    public int addSpellPoint()
    {
        return addPoint(spellXP);
    }

    public int addAgilityPoint()
    {
        return addPoint(agilityXP);
    }

    public int addClassPoint()
    {
        return classXP.levelUp();
    }
    
    public int getClassXPPoints()
    {
        return (int)(classXP.level / 5) + 1;
    }

    public int getCombatXPPoints()
    {
        return combatXP.level;
    }

    public int getSpellXPPoints()
    {
        return spellXP.level;
    }

    public int getAgilityXPPoints()
    {
        return agilityXP.level;
    }

    public int getClassXP()
    {
        return classXP.experience;
    }

    public int getCombatXP()
    {
        return combatXP.experience;
    }

    public int getSpellXP()
    {
        return spellXP.experience;
    }

    public int getAgilityXP()
    {
        return agilityXP.experience;
    }

    public int getClassLevel()
    {
        return classXP.level;
    }

    public void addClassXP(int xp)
    {
        classXP.addXP(xp);
    }

    public void addCombatXP(int xp)
    {
        combatXP.addXP(xp);
    }

    public void addSpellXP(int xp)
    {
        spellXP.addXP(xp);
    }

    public void addAgilityXP(int xp)
    {
        agilityXP.addXP(xp);
    }
}
