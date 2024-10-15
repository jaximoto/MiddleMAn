using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarManager : MonoBehaviour
{
    Calendar calendar;
    public RequestManager requestManager;
    public CalendarView calendarHudUI;
    
    private void Awake()
    {
        calendar = new Calendar();


        
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
        if (Input.GetKey(KeyCode.A))
        {
           CreateRequest("Castle", RelationType.King);
        }
    }
    public void EndDay()
    {
        calendar.NextDay();

        // Update requestManager
        requestManager.currentDay = calendar.day;
        requestManager.maxDayInMonth = calendar.daysInCurrentMonth;

        // Update UI
        calendarHudUI.UpdateDay(calendar.day);
    }

    private void CreateRequest(string buildingName, RelationType relationType)
    {
        // Get the key and update the Calendar UI with the key
        RequestInfo reqKey = requestManager.AddRequest(buildingName, relationType);
        if ( reqKey != null )
        {
            calendarHudUI.AddRequestToCalendar(reqKey);
        }
        
    }
    
}
