using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CalendarView : MonoBehaviour
{
	public TextMeshProUGUI dayCounter;
	public TextMeshProUGUI dayAdvanceText;


	public void UpdateDay(int newDay)
	{
		dayCounter.text = $"Day {newDay}";
		StartCoroutine(DisplayNextDayText(newDay-1));
	}


	private IEnumerator DisplayNextDayText(int day)
	{
		dayAdvanceText.text = $"Day {day} has ended...";

		float dur = 0.0f;
		while (dur < 1.0f)
		{
			yield return new WaitForSeconds(Time.deltaTime);
			dur += Time.deltaTime;
		}

		dayAdvanceText.text = "";

	}

}
