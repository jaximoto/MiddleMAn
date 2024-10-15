using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Calendar
{
    public int day;
    public int month;
    public int year;

    public int daysInCurrentMonth = 30;
    public int monthsInYear = 12;

    public CalendarMonths calendarMonths = new CalendarMonths();

    public Calendar()
    {
        this.day = 1;
        this.month = 1;
        this.year = 1475;
    }

    public void NextDay()
    {
        day++;
        // Call event for day change
        if (day > daysInCurrentMonth)
        {
            // Check for the amount of days of the next month
            day = 1;
            
            if (month > monthsInYear)
            {
                // call event for year change
                month = 1;
                daysInCurrentMonth = calendarMonths.months[month].numberOfDays;
                year++;

            }
            else
            {
                daysInCurrentMonth = calendarMonths.months[++month].numberOfDays;
            }
            // call event for month change
        }


    }
}

