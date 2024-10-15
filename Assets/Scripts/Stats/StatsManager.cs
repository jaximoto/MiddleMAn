using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    
    

    // Starting stats
    public int money;
    public int workers;
    public int availableWorkers;
    public int productivity;
    public int kingAffinity;

    // Stores of stats & data
    public Dictionary<RelationType, Relation> playerRelations = new();
    public Dictionary<StatType, IntStat> playerStats = new();
    
        

    // Events
    public static event Action<StatType, int> OnStatChanged; 
    public static event Action<RelationType, int> OnRelationChanged;
    private void Awake()
    {
        playerRelations.Add(RelationType.King, new Relation(kingAffinity));

        playerStats.Add(StatType.money, new IntStat(StatType.money, money));
        playerStats.Add(StatType.workers, new IntStat(StatType.workers, workers));
        playerStats.Add(StatType.availableWorkers, new IntStat(StatType.availableWorkers, workers));
        playerStats.Add(StatType.productivity, new ClampedStat(StatType.productivity, productivity));

        
    }

    public int GetStatValue(StatType statType)
    {
        return playerStats.ContainsKey(statType) ? playerStats[statType].Value : 0;
    }

    public int GetRelationAptitude(RelationType relationType)
    {
        return playerRelations.ContainsKey(relationType) ? playerRelations[relationType].affinity : 0;
    }

    public void ChangeStat(StatType statType, int newValue)
    {
        if (playerStats.ContainsKey(statType))
        {
            // Apply change to int stat
            playerStats[statType].ChangeValue(newValue);

            // Call event that something changed
            OnStatChanged?.Invoke(statType, playerStats[statType].Value);
                

        }

        else
        {
            Debug.LogError($"PlayerStats does not contain key {statType}");
        }
    }

    public void ChangeAptitude(RelationType relation,  int newValue)
    {
        if (playerRelations.ContainsKey(relation))
        {
            playerRelations[relation].ChangeAffinity(newValue);

            OnRelationChanged?.Invoke(relation, playerRelations[relation].affinity);
        }

        else
        {
            Debug.LogError($"PlayerRelations does not contain key {relation}");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeStat(StatType.money, 5);
            ChangeStat(StatType.workers, 5);
            ChangeStat(StatType.productivity, 5);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeAptitude(RelationType.King, 5);
        }
    }
}
