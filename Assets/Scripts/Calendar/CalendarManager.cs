using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CalendarManager : MonoBehaviour
{
    public BuildingController buildingController;
    public StatsManager statsManager;
    Calendar calendar;
    public RequestManager requestManager;
    public CalendarView calendarHudUI;
    public List<Sprite> monthSprites;
    // building mapped to list of due dates
    public Dictionary<string, List<int>> BuildingToDay = new();
    public List<RelationType> RelationTypes;
    private void Awake()
    {
        calendar = new Calendar();
        RelationTypes = new()
        {
            RelationType.King,
            RelationType.God,
            RelationType.Workers
        };

        
    }

    private void Start()
    {
        requestManager.currentDay = calendar.day;
        calendarHudUI.SetupCalendarUI(calendar.daysInCurrentMonth);
        Debug.Log($"calendar days in month: {calendar.daysInCurrentMonth}");
        requestManager.maxDayInMonth = calendar.daysInCurrentMonth;
        //CreateRequest("Castle", RelationType.King);

    }

    private void Update()
    {
        // TODO REMOVE
        /*
        if (Input.GetKey(KeyCode.A))
        {
           CreateRequest("Castle", RelationType.King);
        }
        */
    }
    public void EndDay()
    {
        List<RequestInfo> markedForRemoval = new();
        // Check if any deadlines were due today
        // Check the dictionary for the day by using get keys
        if (requestManager.requestDictionary.ContainsKey(calendar.day))
        {
            foreach(string key in requestManager.requestDictionary[calendar.day].Keys)
            {
                statsManager.ChangeAptitude(requestManager.requestDictionary[calendar.day][key].relationType, requestManager.requestDictionary[calendar.day][key].GetPenalty());
                //playerRelations[requestManager.requestDictionary[calendar.day][key].relationType].affinity <= 0
                if (statsManager.CheckLose())
                {
                    Debug.Log("You lose the game");
                }
                markedForRemoval.Add(new RequestInfo(calendar.day, requestManager.requestDictionary[calendar.day][key].buildingName));
               
                
            }

            for (int  i = 0; i < markedForRemoval.Count; i++)
            {
                RequestInfo info = markedForRemoval[i];
                ClearRequest(info.buildingName, info.dayScheduled);
            }
        }

        calendar.NextDay();

        // Update requestManager
        requestManager.currentDay = calendar.day;
        requestManager.maxDayInMonth = calendar.daysInCurrentMonth;

        // Add new random request
        CreateRequest(buildingController.model.buildingNames[Random.Range(0, buildingController.model.buildingNames.Count())], RelationTypes[Random.Range(0, RelationTypes.Count)]);

        // Update UI
        calendarHudUI.UpdateDay(calendar.day);
    }

    public void ClearRequest(string buildName, int day)
    {
        BuildingToDay[buildName].Remove(day);
        requestManager.requestDictionary[day].Remove(buildName);
    }
    private void CreateRequest(string buildingName, RelationType relationType)
    {
        // Get the key and update the Calendar UI with the key
        RequestInfo reqKey = requestManager.AddRequest(buildingName, relationType);
        if ( reqKey != null )
        {
            if (!BuildingToDay.ContainsKey(reqKey.buildingName))
            {
                BuildingToDay.Add(reqKey.buildingName, new List<int>()
                {
                    reqKey.dayScheduled
                }); 
            }
            else
            {
                BuildingToDay[reqKey.buildingName].Add(reqKey.dayScheduled);
            }
           
            
           
            calendarHudUI.AddRequestToCalendar(reqKey);

        }
        
    }
    
}
