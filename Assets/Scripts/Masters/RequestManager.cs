using Buildings;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    
    public int currentDay;
    public StatsManager statsManager;

    // Day : dict<requestID, request>
    Dictionary<int, Dictionary<int, GenericRequest>> requestDictionary = new();

    Dictionary<string, Building> buildingDict;

    PlayerWorkerStats workerStats;

    public RequestFactory RequestFactory;

    private void Awake()
    {
        buildingDict = new()
        {
            { "Castle", new Castle() },
            { "Bathhouse", new Bathhouse() }
        };
        RequestFactory = new RequestFactory();
       
    }

    private void Start()
    {
        workerStats = new PlayerWorkerStats(statsManager.GetStatValue(StatType.workers), statsManager.GetStatValue(StatType.productivity));
        //KingRequest newKingRequest = (KingRequest)RequestFactory.CreateRequestWithRelation(buildingDict["Bathhouse"], workerStats, currentDay, RelationType.King);

        AddRequest("Bathhouse", RelationType.God);
        
        //Debug.Log($"newKingRequest has reward of {requestDictionary[request.deadline][request.requestID].GetReward()} and an importance value of {newKingRequest.importance} and scheduled for {newKingRequest.deadline}");
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.V)) 
        { 
            Bathhouse bathouse = new Bathhouse();
            
            

           


            //Debug.Log($"newKingRequest has reward of {newKingRequest.GetReward()} and an importance value of {newKingRequest.importance} and scheduled for {newKingRequest.deadline}");
        }
    }

    public int AddRequest(string buildingType, RelationType relationType)
    {
        if (!buildingDict.ContainsKey(buildingType))
        {
            Debug.LogError($"Error, Building dict does not contain key {buildingType}");
            return -1;
        }

        // Make sure there is not more than 3 requests in day
        GenericRequest request = RequestFactory.CreateRequestWithRelation(buildingDict[buildingType], workerStats, currentDay, relationType);

        if (requestDictionary.ContainsKey(currentDay))
        {
            if (requestDictionary[request.deadline].Count == 3)  
            {
                Debug.Log($"Only 3 requests can be made on a day, there are currently {requestDictionary[request.deadline].Count} requests on day: {request.deadline}");
                return -1;
            }
            // If it already has a key and has less than 3 items in inner dict
            requestDictionary[request.deadline].Add(request.requestID, request);
            return request.requestID;
        }

        requestDictionary[request.deadline] = new Dictionary<int, GenericRequest>();
        requestDictionary[request.deadline][request.requestID] = request;

        Debug.Log($"Added request {requestDictionary[request.deadline][request.requestID].requestID}");
        return request.requestID;

    }
}
