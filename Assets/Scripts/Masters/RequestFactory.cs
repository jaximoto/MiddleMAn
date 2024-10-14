using Buildings;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public interface IRequest
{
    ImportanceFactor GetImportance(float timeWeight, float moneyWeight);
    int GetDeadline(float timeWeight, float moneyWeight, float importanceFactor);

    

    
}

public struct BuildingConstraints
public class GenericRequest : IRequest
{
    public float buildTime;
    public float maxBuildTime;

    int cost;
    public int maxCost;

    int currentDay;
    public int maxDeadline;

    public ImportanceFactor importance;
    public RelationType relationType;

    public int deadline;

    public GenericRequest(float timeWeight, float moneyWeight, 
        float importanceWeight, float buildTime,float maxBuildTime, int cost,
        int maxCost, int currentDay, int maxDeadline, RelationType relationType)
    {
        this.buildTime = buildTime;
        this.maxBuildTime = maxBuildTime;
        this.cost = cost;
        this.maxCost = maxCost;
        this.currentDay = currentDay;
        this.maxDeadline = maxDeadline;
        this.importance = GetImportance(timeWeight, moneyWeight);
        this.relationType = relationType;
        this.deadline = GetDeadline(timeWeight, moneyWeight, importanceWeight);
    }

    
    public ImportanceFactor GetImportance(float timeWeight, float moneyWeight)
    {
        float timeValue = timeWeight * (buildTime / maxBuildTime);
        float costValue = moneyWeight * (cost / maxCost);

        int result =  Mathf.CeilToInt(1 + 9 * (timeValue + costValue));

        if (result < 1)
        {
            return ImportanceFactor.Negligible;
        }

        else if ( result > 10)
        {
            return ImportanceFactor.Maximum;
        }
        else
        {
            return (ImportanceFactor)result;
        }
        
    }

    public int GetDeadline(float timeWeight, float moneyWeight, float importanceFactor)
    {
        float timeValue = timeWeight * (buildTime / maxBuildTime);
        float costValue = moneyWeight * (cost / maxCost);
        float importanceValue = importanceFactor * (importanceFactor / 10);
        int deadlineValue = maxDeadline - currentDay;

        int result = Mathf.CeilToInt(currentDay + (timeValue + costValue + importanceValue) * deadlineValue);
        
        if (result < currentDay)
        {
            return currentDay;
        }

        else if (result > maxDeadline)
        {
            return maxDeadline;
        }
        else
        {
            return result;
        }
    } 
}

public class KingRequest : GenericRequest
{
    public KingRequest(float timeWeight, float moneyWeight, float importanceWeight,
        float buildTime, float maxBuildTime, int cost, int maxCost, int currentDay, int maxDeadline)
        : base(timeWeight, moneyWeight, importanceWeight, buildTime,
            maxBuildTime, cost, maxCost, currentDay, maxDeadline, RelationType.King)
    {

    }
    
}
public abstract class RequestFactory
{
    public RequestFactory(building) 
    {
    }
}
