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
    public int godAffinity;
    public int workerAffinity;

    // Stores of stats & data
    public Dictionary<RelationType, Relation> playerRelations = new();
    public Dictionary<StatType, IntStat> playerStats = new();
    
        

    // Events
    public static event Action<StatType, int> OnStatChanged; 
    public static event Action<RelationType, int> OnRelationChanged;
    private void Awake()
    {

		this.kingAffinity = 70;
		this.godAffinity = 70;
		this.workerAffinity = 70;

        playerRelations.Add(RelationType.King, new Relation(kingAffinity));
        playerRelations.Add(RelationType.God, new Relation(godAffinity));
        playerRelations.Add(RelationType.Workers, new Relation(workerAffinity));

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

	public void UpdateStats()
	{
		// Money based on king affinity
		int kingApt = GetRelationAptitude(RelationType.King);
		ChangeStat(StatType.money, (kingApt - 50) * 100);

		// Productivity based on worker Affinity
		int workerApt = GetRelationAptitude(RelationType.Workers);
		ChangeStat(StatType.productivity, (int)System.Math.Ceiling((workerApt-50) * 0.25));

		//God works in mysterious ways
		int godApt = GetRelationAptitude(RelationType.God);
		ChangeStat(StatType.workers, (int)System.Math.Ceiling((godApt-50) * 0.25));
		ChangeStat(StatType.availableWorkers, (int)System.Math.Ceiling((godApt-50) * 0.25));


	}


	public bool AnyAptitudeZero()
	{
		foreach (Relation relation in playerRelations.Values)
		{
			if (relation.affinity <= 0)
				return true;
		}
		return false;
	}



	public bool CheckLose()
	{
		return (GetStatValue(StatType.money) <= 0) || (AnyAptitudeZero());
	}


    private void Update()
    {
		/*
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
		*/
    }


}
