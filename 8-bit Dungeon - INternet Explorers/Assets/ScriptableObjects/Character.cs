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
    //public MaxHp maxHp;
    //public RecourseStorage recourseStorage;
    public Dictionary<String, Stat> stats = new Dictionary<String, Stat>();
    public string title;
    public int attackRecourseCost;

    public void init()
    {
        string file = Resources.Load<TextAsset>(@"Game Data\classes").text;
        JObject o = JObject.Parse(file).Value<JObject>(title);
        stats.Add("MaxHP", new MaxHp(o.Value<JObject>("HP").Value<int>("Base"), o.Value<JObject>("HP").Value<float>("LvlMult")));
        stats.Add("MaxRecourse", new RecourseStorage(o.Value<JObject>("Recourse").Value<int>("Base"), o.Value<JObject>("Recourse").Value<float>("LvlMult")));
        Debug.Log("TITLE IS : " + title);
        Debug.Log("Strength is:" + o.Value<JObject>("Strength"));
        stats.Add("Strength", new Stat(o.Value<JObject>("Strength").Value<int>("Base"), o.Value<JObject>("Strength").Value<int>("LvlMult")));
        //attackRecourseCost = o.Value<JObject>("AttackCost").Value<int>("Cost");
    }
}
