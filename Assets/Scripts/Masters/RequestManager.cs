using Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    
    public int currentDay;
    public StatsManager statsManager;

    Dictionary<int, Dictionary<int, GenericRequest>> requestDictionary = new();

    Dictionary<string, Building> buildingDict;
   

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
        PlayerWorkerStats workerStats = new PlayerWorkerStats(statsManager.GetStatValue(StatType.workers), statsManager.GetStatValue(StatType.productivity));
        KingRequest newKingRequest = (KingRequest)RequestFactory.CreateRequestWithRelation(buildingDict["Bathhouse"], workerStats, currentDay, RelationType.King);
        Debug.Log($"newKingRequest has reward of {newKingRequest.GetReward()} and an importance value of {newKingRequest.importance} and scheduled for {newKingRequest.deadline}");
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.V)) 
        { 
            Bathhouse bathouse = new Bathhouse();
            
            

           


            //Debug.Log($"newKingRequest has reward of {newKingRequest.GetReward()} and an importance value of {newKingRequest.importance} and scheduled for {newKingRequest.deadline}");
        }
    }
}
