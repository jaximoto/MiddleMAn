using Buildings;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;
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
public class GenericRequest
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

    public GenericRequest(Building buildingInfo, RequestWeights RequestWeights,
        SchedulingParams schedulingParams, RelationType relationType)
    {
        this.buildTime = buildingInfo.buildCost;
        this.maxBuildTime = BuildingConstraints.MaxBuildTime;
        this.cost = buildingInfo.moneyCost;
        this.maxCost = BuildingConstraints.MaxCost;
        this.currentDay = schedulingParams.currentDay;
        this.maxDeadline = schedulingParams.maxDeadline;
        this.importance = GetImportance(RequestWeights);
        this.relationType = relationType;
        this.deadline = GetDeadline(RequestWeights, this.importance);
    }

    
    public ImportanceFactor GetImportance(RequestWeights RequestWeights)
    {
        Debug.Log($"timeWeight = {RequestWeights.time}, costweight = {RequestWeights.money}");
        Debug.Log($"buildTime = {this.buildTime}, buildCost = {this.cost}");
        float timeValue = RequestWeights.time * (buildTime / maxBuildTime);
        float costValue = RequestWeights.money * (cost / maxCost);
        Debug.Log($"timeValue = {timeValue}, costValue = {costValue}");
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

    public int GetDeadline(RequestWeights requestWeights, ImportanceFactor importance)
    {
        float timeValue = requestWeights.time * (buildTime / maxBuildTime);
        float costValue = requestWeights.money * (cost / maxCost);
        float importanceValue = requestWeights.importance * ((int)importance / 10);
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
    public int GetReward()
    {
        int importanceMultiplier = (int)importance;
        return 5 * importanceMultiplier;
    }

    public int GetPenalty()
    {
        int importanceMultiplier = (int)importance;
        return -5 * importanceMultiplier;
    }
}

public class KingRequest : GenericRequest
{
    public KingRequest(Building buildingInfo, RequestWeights RequestWeights,
        SchedulingParams schedulingParams)
        : base(buildingInfo, RequestWeights, schedulingParams, RelationType.King)
    {
        // Fuck you
    }
    
    
}

public class GodRequest : GenericRequest
{
    public GodRequest(Building buildingInfo, RequestWeights RequestWeights,
        SchedulingParams schedulingParams)
        : base(buildingInfo, RequestWeights, schedulingParams, RelationType.God)
    {
        // Eat 
    }
}

/*
public abstract class RequestFactory
{
    public abstract GenericRequest CreateRequest();

    public Building buildInfo;
    public RequestWeights RequestWeights;
    public SchedulingParams schedulingParams;


    public RequestFactory(Building buildingInfo, RequestWeights RequestWeights,
        SchedulingParams schedulingParams)
    {
        this.buildInfo = buildingInfo;
        this.RequestWeights = RequestWeights;
        this.schedulingParams = schedulingParams;
    }
  
}

public class KingRequestFactory : RequestFactory
{
    public KingRequestFactory(Building buildingInfo, RequestWeights RequestWeights,
        SchedulingParams schedulingParams) : base(buildingInfo, RequestWeights, schedulingParams)
    {
        // My
    }

    public override GenericRequest CreateRequest()
    {
        return new KingRequest(this.buildInfo, this.RequestWeights, this.schedulingParams);
    }
}

public class GodRequestFactory : RequestFactory
{
    public GodRequestFactory(Building buildingInfo, RequestWeights RequestWeights,
        SchedulingParams schedulingParams) : base(buildingInfo, RequestWeights, schedulingParams)
    {
        // Dick
    }

    public override GenericRequest CreateRequest()
    {
        return new GodRequest(this.buildInfo, this.RequestWeights, this.schedulingParams);
    }
}
*/

public class RequestFactory
{
    public GenericRequest CreateRequestWithRelation(Building buildingInfo, RequestWeights RequestWeights,
        SchedulingParams schedulingParams, RelationType relation)
    {
       
        
        if (relation == RelationType.King)
        {
            return new KingRequest(buildingInfo, RequestWeights, schedulingParams);
        }
        
        else if ( relation == RelationType.God)
        {
            return new GodRequest(buildingInfo, RequestWeights, schedulingParams);
        }
                
        else
        {
            Debug.LogError($"Error, Request Factory doesn't recognize: {relation}");
            return null;
        }
           
        
    }
}


