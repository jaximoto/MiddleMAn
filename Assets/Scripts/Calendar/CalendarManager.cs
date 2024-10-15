using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarManager : MonoBehaviour
{
    Calendar calendar;
    RequestManager requestManager;
    private void Awake()
    {
        calendar = new Calendar();

        
    }

    
    void NextDay()
    {
        calendar.NextDay();
    }

    
}
