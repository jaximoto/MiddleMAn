using Buildings;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    
    public int currentDay;
    public int maxDayInMonth;
    public StatsManager statsManager;
    public BuildingController buildingController;

    // Day : dict<requestID, request>
    public Dictionary<int, Dictionary<string, GenericRequest>> requestDictionary = new();

    

   

    PlayerWorkerStats workerStats;

    public RequestFactory RequestFactory;

    private void Awake()
    {
       
        RequestFactory = new RequestFactory();
       
    }

    private void Start()
    {
        workerStats = new PlayerWorkerStats(statsManager.GetStatValue(StatType.workers), statsManager.GetStatValue(StatType.productivity));
        //KingRequest newKingRequest = (KingRequest)RequestFactory.CreateRequestWithRelation(buildingDict["Bathhouse"], workerStats, currentDay, RelationType.King);

        //AddRequest("Bathhouse", RelationType.God);
        //AddRequest("Castle", RelationType.King);
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

    public RequestInfo AddRequest(string buildingType, RelationType relationType)
    {
        if (!buildingController.model.buildingsMap.ContainsKey(buildingType))
        {
            Debug.LogError($"Error, Building dict does not contain key {buildingType}");
            return null;
        }

        // Make sure there is not more than 3 requests in day
        GenericRequest request = RequestFactory.CreateRequestWithRelation(buildingController.model.buildingsMap[buildingType], workerStats, currentDay, relationType);

        if (request.deadline > maxDayInMonth)
        {
            Debug.Log($"Request has deadline: {request.deadline} which is greater than {maxDayInMonth}");
            return null;
        }
            

        if (requestDictionary.ContainsKey(request.deadline))
        {
            if (requestDictionary[request.deadline].Count >= 3)  
            {
                Debug.Log($"Only 3 requests can be made on a day, there are currently {requestDictionary[request.deadline].Count} requests on day: {request.deadline}");
                return null;
            }

            if (requestDictionary[request.deadline].ContainsKey(request.buildingName))
            {
                Debug.Log($"{request.deadline} can only have one building: {request.buildingName} as a key");
                return null;
            }
            // If it already has a key and has less than 3 items in inner dict
            requestDictionary[request.deadline].Add(request.buildingName, request);
            Debug.Log($"Added request {requestDictionary[request.deadline][request.buildingName].buildingName} on day {request.deadline} with count: {requestDictionary[request.deadline].Count}");
            return new RequestInfo(request.deadline, request.buildingName);
        }

        requestDictionary[request.deadline] = new Dictionary<string, GenericRequest>();
        requestDictionary[request.deadline][request.buildingName] = request;

        
        Debug.Log($"Added request {requestDictionary[request.deadline][request.buildingName].buildingName} on day {request.deadline} with count: {requestDictionary[request.deadline].Count}");
        return new RequestInfo(request.deadline, request.buildingName);

    }
}
