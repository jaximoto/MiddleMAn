using Buildings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    public float buildTimeWeight, buildCostWeight, importanceWeight;

    Dictionary<int, Queue<GenericRequest>> requests;

   public RequestFactory RequestFactory;

    private void Awake()
    {
        RequestFactory = new RequestFactory();
        
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.V)) 
        { 
            Bathhouse bathouse = new Bathhouse();
            RequestWeights reqWeights = new RequestWeights(buildTimeWeight, buildCostWeight, importanceWeight);
            SchedulingParams schedulingParams = new SchedulingParams(1, 30);
            KingRequest newKingRequest =  (KingRequest)RequestFactory.CreateRequestWithRelation(bathouse, reqWeights, schedulingParams, RelationType.King);


            Debug.Log($"newKingRequest has reward of {newKingRequest.GetReward()} and an importance value of {newKingRequest.importance} and scheduled for {newKingRequest.deadline}");
        }
    }
}
