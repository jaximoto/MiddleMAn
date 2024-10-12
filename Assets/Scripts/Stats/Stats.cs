using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public int money = 1000;
    public int workers = 10;
    public int productivity = 50;

    public List<Relation> relationList = new();

    Stats(BaseStats baseStats)
    {
        money = baseStats.money;
        workers = baseStats.workers;
        productivity = baseStats.productivity;
        
        for (int i = 0; i < baseStats.relations.Count; i++)
        {

            relationList.Add(new Relation(baseStats.relations[i].name, baseStats.relations[i].affinity));
        }
    }
}
