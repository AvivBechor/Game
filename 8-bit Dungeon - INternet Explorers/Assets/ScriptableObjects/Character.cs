using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public MaxHp maxHp;
    public RecourseStorage recourseStorage;
    public string title;
    public int attackRecourseCost;

    public void init()
    {
        string file = File.ReadAllText(Directory.GetCurrentDirectory() + @"\Assets\Game Data\classes.json");
        JObject o = JObject.Parse(file);
        this.maxHp.value = o.Value<JObject>(title).Value<JObject>("HP").Value<int>("Base");
        this.maxHp.statMultiplayer = o.Value<JObject>(title).Value<JObject>("HP").Value<float>("LvlMult");
        this.recourseStorage.value = o.Value<JObject>(title).Value<JObject>("Recourse").Value<int>("Base");
        this.recourseStorage.statMultiplayer = o.Value<JObject>(title).Value<JObject>("Recourse").Value<float>("LvlMult");
        this.attackRecourseCost = o.Value<JObject>(title).Value<JObject>("AttackCost").Value<int>("Cost");
    }
}
