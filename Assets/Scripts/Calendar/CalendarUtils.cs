using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Month
{
    public string name;
    public int numberOfDays;

    public Month(string name, int numOfDays)
    {
        this.name = name;
        this.numberOfDays = numOfDays;
    }
}
public class CalendarMonths
{
    public List<Month> months = new()
    {
            new Month("January", 31),
            new Month("February", 28), // Adjust for leap years if needed
            new Month("March", 31),
            new Month("April", 30),
            new Month("May", 31),
            new Month("June", 30),
            new Month("July", 31),
            new Month("August", 31),
            new Month("September", 30),
            new Month("October", 31),
            new Month("November", 30),
            new Month("December", 31)
    };

}
