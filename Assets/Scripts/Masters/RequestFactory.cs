using Buildings;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;
public enum ImportanceFactor
{
    Maximum = 1,       // Level 1
    Critical = 2,      // Level 2
    Severe = 3,        // Level 3
    High = 4,          // Level 4
    Substantial = 5,   // Level 5
    Moderate = 6,      // Level 6
    Slight = 7,        // Level 7
    Low = 8,           // Level 8
    Unimportant = 9,   // Level 9
    Negligible = 10    // Level 10
}

public class RequestInfo
{
    public string buildingName;
    public int dayScheduled;

    public RequestInfo(int dayScheduled,string buildingName)
    {
        this.dayScheduled = dayScheduled;
        this.buildingName = buildingName;
    }

}
public class GenericRequest
{
    public string buildingName;
    
    public float buildTime;
    public float maxBuildTime;

    int totalWorkers;
    int productivity;
    int cost;
    public int maxCost;

    int currentDay;
    public int maxDeadline;

    public ImportanceFactor importance;
    public RelationType relationType;

    public int deadline;

    public GenericRequest(Building buildingInfo, PlayerWorkerStats workerStats, ImportanceFactor importance,
        int currentDay, RelationType relationType)
    {
        this.buildingName = buildingInfo.name;
        this.totalWorkers = workerStats.workers;
        this.productivity = workerStats.productivity;
        this.currentDay = currentDay;
        this.buildTime = buildingInfo.buildCost;
        this.importance = importance;
        this.relationType = relationType;
        this.deadline = GetDeadline();
    }

    
    /*
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
    */
    public int GetDeadline()
    {
        int baseTimeCalc = Mathf.CeilToInt(this.buildTime / (this.totalWorkers * .5f * this.productivity));
        //Debug.Log($"BuildTime: {this.buildTime}");



        int adjustedBuildTime = baseTimeCalc + 1 * (int)this.importance;

        //Debug.Log($"adjusted BuildTime {adjustedBuildTime}");

        //int maxTimeCalc = Mathf.CeilToInt(adjustedBuildTime / (this.totalWorkers * .5f * this.productivity));
        Debug.Log($"baseTimeCalc {baseTimeCalc} adjustedBuildTime {adjustedBuildTime}");
        return Random.Range(baseTimeCalc, adjustedBuildTime);
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
    public KingRequest(Building buildingInfo, PlayerWorkerStats workerStats, ImportanceFactor importance,
        int currentDay)
        : base(buildingInfo, workerStats, importance, currentDay, RelationType.King)
    {
        // Fuck you
    }
    
    
}

public class GodRequest : GenericRequest
{
    public GodRequest(Building buildingInfo, PlayerWorkerStats workerStats, ImportanceFactor importance,
       int currentDay)
        : base(buildingInfo, workerStats, importance, currentDay, RelationType.God)
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
    public GenericRequest CreateRequestWithRelation(Building buildingInfo, PlayerWorkerStats workerStats,
        int currentDay, RelationType relation)
    {
        ImportanceFactor importance = (ImportanceFactor)Random.Range(1, 11);
        
        if (relation == RelationType.King)
        {
            return new KingRequest(buildingInfo, workerStats, importance, currentDay);
        }
        
        else if ( relation == RelationType.God)
        {
            return new GodRequest(buildingInfo, workerStats, importance, currentDay);
        }
                
        else
        {
            Debug.LogError($"Error, Request Factory doesn't recognize: {relation}");
            return null;
        }
           
        
    }
}


