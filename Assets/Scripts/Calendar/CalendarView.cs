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

	public GameObject taskBoxContainer;
	float taskboxStartingX = -153;
	float taskboxStartingY = 73;	
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

		// Get request and instantiate a ui element at the correct place and set its parent and scale
		if (tmp != null)
		{
			float x;
			float y;

            if (requestInfo.dayScheduled % 7 != 0)
			{
                x = taskboxStartingX + (51f * ((requestInfo.dayScheduled % 7) - 1));
				
            }
			else
			{
				x = taskboxStartingX + 51f * 6;
				
			}
            //x = taskboxStartingX + (52f * ((requestInfo.dayScheduled % 7) - 1));
            //float xPositionIndex = taskboxStartingX + 52f * (requestInfo.dayScheduled - 1) % 7 + 1;  // X cycles through 1-7
            //float yPositionIndex = taskboxStartingY - 55f * (requestInfo.dayScheduled - 1) / 7;  // Y increases after every 7 days
			
			if (requestInfo.dayScheduled % 7 == 0)
			{
				y = taskboxStartingY;
            }
			else
			{
                y = taskboxStartingY - 55f * (requestInfo.dayScheduled / 7);
            }
             //y = taskboxStartingY - 55f *  (requestInfo.dayScheduled / 7);
			
            GameObject container = Instantiate(taskBoxContainer, new Vector3(x, y, 1f), Quaternion.identity);
			// set image
			container.GetComponent<Image>().sprite = taskBackgrounds[(int)tmp.relationType];

			// Set Text
			container.GetComponentInChildren<TMP_Text>().text = tmp.buildingName;

			// parent bs
			container.transform.SetParent(calendarUI.transform);
			container.transform.localScale = Vector3.one;
			container.transform.localPosition = new Vector3(x, y, 1f);
		}

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
