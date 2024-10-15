using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

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

	public Building currentBuilding;

	public TMP_InputField workerAllocator;

    // Start is called before the first frame update
    void Start()
    {
		this.model = new BuildingModel();
    }


	void Update()
	{

		EquipBuilding();

		if (Input.GetMouseButtonDown(0)) //TODO Need some way to make it so the fucking player cant just spam click like a fucking monkey
		{
			HandleClick();
		}

		HighlightTile();

		if (currentBuilding!=null)
			view.RefreshBuildingUI(currentBuilding);
	}


	public void HighlightTile()
	{
		Vector3Int cell = GetTileCoordinates();
		Tile tile = (Tile)(tilemap.GetTile(cell));
		if (tile==null) return;

		view.HighlightTile(cell, model.lastHighlightedCell, model.lastCellColor);
		model.lastHighlightedCell = cell;
		model.lastCellColor = tile.color;
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
		string currentBuildingName = model.buildingOptions.Current();
		Building b = BuildingFactory.MakeBuilding(currentBuildingName, pos, tilemap);
		if (CheckStats(b))
		{
			model.AddBuilding(b);
			view.UpdateBuilding(b);
			MakeBuildingModifyStats(b);
		}
		else
		{
			view.UpdateNotifyText($"Need ${b.moneyCost} to build a {b.name}.");
		}
	}


	public bool CheckStats(Building b)
	{
		bool enoughMoney = b.moneyCost <= statsManager.GetStatValue(StatType.money);
		return enoughMoney; 
	}


	public void MakeBuildingModifyStats(Building b)
	{
		statsManager.ChangeStat(StatType.money, -b.moneyCost);
		//TODO: Change active workers
	}


	public bool CheckForTiles(List<Vector3Int> coords)
	{
		foreach (Vector3Int coord in coords)
		{
			if (!tilemap.HasTile(coord))
				return false;
		}
		return true;
	}
		

	public void HandleClick()
	{

		Vector3Int clickedCell = GetTileCoordinates();

		// First, am I clicking a building?

		if (model.occupiedTiles.Contains(clickedCell))
		{
			HandleClickBuilding(model.buildings[clickedCell]);
			return;
		}

		// Coordinates this building will occupy
		List<Vector3Int> coords = model.buildingsMap[model.equippedBuildingName].EnumerateCoordinates(clickedCell);

		// Do nothing if building spills outside of tilemap
		if (!CheckForTiles(coords)) return;

		//Check to see if building spills onto an occupied tile 
		if (model.CheckForBuilding(coords))
		{
		}
		else 
		{
			// No building here, call logic to add one 
			MakeBuilding(clickedCell);
		}
		
	}


	public void HandleClickBuilding(Building b)
	{
		currentBuilding = b;
	}


	public void AllocateWorkers()
	{
		if (currentBuilding==null) return;

	  	if (currentBuilding.IsDone()) 
		{
			view.UpdateNotifyText("This building is done");
		}

		int newWorkers = -1;
		bool validInput = System.Int32.TryParse(workerAllocator.text, out newWorkers);

		if (validInput)
		{
			if (newWorkers > statsManager.GetStatValue(StatType.availableWorkers))
			{
				view.UpdateNotifyText("You don't have enough workers");
			}
			else
			{
				currentBuilding.assignedWorkers = newWorkers;
				statsManager.ChangeStat(StatType.availableWorkers, -newWorkers);
			}
		}
		else
		{
			view.UpdateNotifyText("Just put a number and nothing else in");
		}

		view.ClearWorkerAllocator();
	}


	//TODO: Shit fucking name
	private Vector3Int GetTileCoordinates()
	{
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3Int clickedCell = tilemap.WorldToCell(mouseWorldPos);
		return clickedCell;
	}	


	private float ComputeProgress(Building b)
	{
		return b.assignedWorkers * statsManager.GetStatValue(StatType.productivity);
	}


	public void AdvanceBuildingStates()
	{
		//TODO
		foreach (Building b in model.buildings.Values)
		{
			float progress = ComputeProgress(b);
			b.AdvanceState(progress);
			view.UpdateBuilding(b); 

			if (b.IsDone())
			{
				statsManager.ChangeStat(StatType.availableWorkers, b.assignedWorkers);
				b.assignedWorkers = 0;
			}
		}
	}

}
