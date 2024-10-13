using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarModel 
{
	public int day;


	public void Initialize()
	{
		this.day = 0;
	}


	public void IncrementDay()
	{
		day++;
	}

}
