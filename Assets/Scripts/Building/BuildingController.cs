using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Utils;
using Buildings;

using static BuildingView;
using static BuildingModel;
using static StatsManager;


public class BuildingController : MonoBehaviour
{
	public BuildingModel model;
	public BuildingView view;
	public Tilemap tilemap;
	public StatsManager statsManager;


    // Start is called before the first frame update
    void Start()
    {
		this.model = new BuildingModel();
    }


	void Update()
	{
		HighlightTile();

		EquipBuilding();

		if (Input.GetMouseButtonDown(0)) //TODO Need some way to make it so the fucking player cant just spam click like a fucking monkey
		{
			//TODO: Check to see if can click by talking to other systems
			HandleClick();
		}
	}


	public void HighlightTile()
	{
		Vector3Int cell = GetTileCoordinates();
		view.HighlightTile(cell);
	}


	public void EquipBuilding()
	{
		//Scroll up
		if (Input.mouseScrollDelta.y > 0)
		{
			model.buildingOptions.Forward();
		} //Scroll down
		else if (Input.mouseScrollDelta.y < 0)
		{
			model.buildingOptions.Backward();
		}

		model.EquipBuilding();

		view.UpdateEquippedBuilding(model.dummyBuilding);
	}


	public void MakeBuilding(Vector3Int pos)
	{
		// This string checking thing fucking horrific
		string currentBuildingName = model.buildingOptions.Current();
		if (currentBuildingName.Equals("Bathhouse"))
		{
			Building b = new Bathhouse(pos, tilemap);
			model.AddBuilding(b);
			view.UpdateBuilding(b);
			MakeBuildingModifyStats(b);
		}
		if (currentBuildingName.Equals("Castle"))
		{
			Building b = new Castle(pos, tilemap);
			model.AddBuilding(b);
			view.UpdateBuilding(b);
			MakeBuildingModifyStats(b);
		}
		//etc...

	}


	public void MakeBuildingModifyStats(Building b)
	{
		statsManager.ChangeStat(StatType.money, -b.moneyCost);
	}


	public void HandleClick()
	{
		Vector3Int clickedCell = GetTileCoordinates();

		if (!tilemap.HasTile(clickedCell)) return;

		//Check to see if building on tile
		if (model.CheckForBuilding(clickedCell))
		{
			//If yes, do nothing/bring up some UI thing
		}
		else 
		{
			// No building here, call logic to add one 
			MakeBuilding(clickedCell);
		}
		
	}


	//TODO: Shit fucking name
	private Vector3Int GetTileCoordinates()
	{
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3Int clickedCell = tilemap.WorldToCell(mouseWorldPos);
		return clickedCell;
	}	


	private float ComputeProgress()
	{
		return statsManager.GetStatValue(StatType.workers) * statsManager.GetStatValue(StatType.productivity);
	}


	public void AdvanceBuildingStates()
	{
		//TODO
		float progress = ComputeProgress();
		foreach (Building b in model.buildings.Values)
		{
			b.AdvanceState(progress);
			view.UpdateBuilding(b); 
		}
	}

}
