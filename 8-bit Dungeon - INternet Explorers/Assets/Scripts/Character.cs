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
    public string title;

    public void init()
    {
        string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string file = File.ReadAllText(appdata + @"\Internet explorers\classes.json");
        JObject o = JObject.Parse(file);
        this.maxHp.value = o.Value<JObject>(title).Value<JObject>("HP").Value<int>("Base");
        this.maxHp.statMultiplayer = o.Value<JObject>(title).Value<JObject>("HP").Value<float>("LvlMult");
    }
}
