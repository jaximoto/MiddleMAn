using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ImportanceFactor
{
    Negligible = 1,    // Level 1
    Unimportant = 2,       // Level 2
    Low = 3,           // Level 3
    Slight = 4,        // Level 4
    Moderate = 5,      // Level 5
    Substantial = 6,    // Level 6
    High = 7,          // Level 7
    Severe = 8,      // Level 8
    Critical = 9,      // Level 9
    Maximum = 10       // Level 10
}
public abstract class BaseMasterRequest
{
    public RelationType relationType;
    public ImportanceFactor importance;
    public string buildingName;
    public BaseMasterRequest(RelationType relationType, ImportanceFactor importance, string buildingName)
    {
        this.relationType = relationType;
        this.importance = importance;
        this.buildingName = buildingName;
    }

    public abstract int GetReward();
    public abstract int GetPenalty();
}

public class MasterRequest: BaseMasterRequest
{
    public MasterRequest(RelationType relationType, ImportanceFactor importance, string buildingName)
        : base(relationType, importance, buildingName)
    {
        return;
    }
    public override int GetReward()
    {
        int importanceMultiplier = (int)importance;
        return 5 * importanceMultiplier;
    }

    public override int GetPenalty()
    {
        int importanceMultiplier = (int)importance;
        return -5 * importanceMultiplier;
    }
}
