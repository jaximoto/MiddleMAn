using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CalendarView : MonoBehaviour
{
	public TextMeshProUGUI dayCounter;
	public TextMeshProUGUI notificationText;
    
	public List<Sprite> dayNumbers = new();
	public List<Sprite> taskBackgrounds = new();
	public CalendarManager calendarManager;
	public RequestManager requestManager;

	public GameObject calendarUI;
	public GameObject dayContainer;
	float startingX = 461;
	float startingY = 540;

	float taskboxStartingX = -154;
	float taskboxStartingY = 74;	
	public void SetupCalendarUI(int daysInMonth)
	{
		GameObject parentObject = new GameObject("DaysParent");
		parentObject.transform.SetParent(calendarUI.transform);
		float currentX = startingX;
		float currentY = startingY;

		for (int i = 1; i < daysInMonth + 1; i++)
		{
			GameObject tmp = Instantiate(dayContainer, new Vector3(currentX, currentY, 1), Quaternion.identity);
			
			tmp.transform.SetParent(parentObject.transform);

			tmp.transform.localScale = new Vector3(2.5f, 2.5f, 1);
            Image[] spriteArr = tmp.GetComponentsInChildren<Image>();

			// Setup day variables
			spriteArr[0].sprite = dayNumbers[i / 10];
			spriteArr[1].sprite = dayNumbers[i % 10];

			currentX += 128;

			if (i % 7 == 0)
			{
				currentY -= 137;
				currentX = startingX;
			}
		}
	}

	public void AddRequestToCalendar(RequestInfo requestInfo)
	{
		GenericRequest tmp = requestManager.requestDictionary[requestInfo.dayScheduled][requestInfo.buildingName];

	}

	
    public void UpdateDay(int newDay)
	{
		dayCounter.text = $"Day {newDay}";
		StartCoroutine(DisplayNextDayText(newDay-1));
	}


	private IEnumerator DisplayNextDayText(int day)
	{
		notificationText.text = $"Day {day} has ended...";

		float dur = 0.0f;
		while (dur < 1.0f)
		{
			yield return new WaitForSeconds(Time.deltaTime);
			dur += Time.deltaTime;
		}

		notificationText.text = "";

	}

}
