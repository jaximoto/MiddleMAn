using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CalendarModel;
using static CalendarView;


public class CalendarController : MonoBehaviour
{
	public CalendarModel model;
	public CalendarView view;

	
	public enum State
	{
		canAdvanceDay, //Shitty name
		advancingDay
	}

	public State state;
	

	void Start()
	{
		this.model = new CalendarModel();
		this.state = State.canAdvanceDay;	
	}


	public void EndDay()
	{
		if (CheckState(State.canAdvanceDay))
		{
			model.IncrementDay(); //Increment asshole width
			view.UpdateDay(model.day);
		}
	}


	private bool CheckState(State s)
	{
		return this.state==s;
	}

	
}
