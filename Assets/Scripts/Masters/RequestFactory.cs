using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRequest
{
    int GetImportance(float timeWeight, float moneyWeight, float timeToBuild, int cost);
    int GetDeadline();

    

    RelationType GetRelationType();
}

public class GenericRequest : IRequest
{
    public float maxBuildTime;

    public int maxCost;
    public int GetDeadline()
    {
        throw new System.NotImplementedException();
    }

    public int GetImportance(float timeWeight, float moneyWeight, float timeToBuild, int cost)
    {
        float timeValue = timeWeight * (timeToBuild / maxBuildTime);
        float costValue = moneyWeight * (cost / maxCost);

        return Mathf.CeilToInt(1 + 9 * (timeValue + costValue));
    }

    public RelationType GetRelationType()
    {
        throw new System.NotImplementedException();
    }

    
}
