using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using TMPro;

using Buildings;

public class BuildingView : MonoBehaviour
{
	public Tilemap tilemap;

	public TextMeshProUGUI equippedBuildingName; 

	public Image equippedBuildingRenderer;

	public TextMeshProUGUI notificationText; 
	public TextMeshProUGUI progressText; 
	public TextMeshProUGUI workerText; 
	public TextMeshProUGUI buildingKindText; 
	public TMP_InputField workerAllocator; 

	public GameObject buildingUI;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public void UpdateBuilding(Building b)
	{
		//This is so fuckign stupid
		for (int i=0; i<b.tiles.Count; i++)
		{
			tilemap.SetTile(b.residentCoordinates[i], b.currentTiles[i]);
			tilemap.RefreshTile(b.residentCoordinates[i]);
		}
	}


	public void UpdateEquippedBuilding(Building b)
	{
		equippedBuildingName.text = b.name;
		equippedBuildingRenderer.sprite = b.completeTile.sprite;
	}

	
	public void HighlightTile(Vector3Int cell, Vector3Int lastHighlightedCell, Color lastColor)
	{
		if (lastHighlightedCell == cell)
			return;

		tilemap.SetTileFlags(cell, TileFlags.None);
		tilemap.SetColor(cell, Color.blue);

		tilemap.SetTileFlags(lastHighlightedCell, TileFlags.None);
		tilemap.SetColor(lastHighlightedCell, lastColor);
	}

	public void UpdateNotifyText(string text)
	{
		StartCoroutine(NotifyText(text));
	}

	private IEnumerator NotifyText(string text)
	{
		notificationText.text = text;

		float dur = 0.0f;
		while (dur < 1.0f)
		{
			yield return new WaitForSeconds(Time.deltaTime);
			dur += Time.deltaTime;
		}

		notificationText.text = "";
	}


	public void RefreshBuildingUI(Building b)
	{
		if (b.IsDone())
		{
			progressText.text = "Done";
			workerText.text = "";
			buildingKindText.text = "";
		}
		else
		{
			progressText.text = $"{(b.buildProgress / b.buildCost) * 100}% progress";
			workerText.text = $"{b.assignedWorkers} workers";
			buildingKindText.text = $"{b.name}";
		}

	}

	public void ClearWorkerAllocator()
	{
		workerAllocator.text = "";
	}

}
