using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Utils;
using Buildings;

using static BuildingView;
using static BuildingModel;


public class BuildingController : MonoBehaviour
{
	public BuildingModel model;
	public BuildingView view;
	public Tilemap tilemap;


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
			//TODO: Check to see if can click by talking to other systems
			HandleClick();
		}
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


	public void MakeBuilding(Vector3Int pos, Tile tile)
	{
		// This string checking thing fucking horrific
		string currentBuildingName = model.buildingOptions.Current();
		if (currentBuildingName.Equals("Bathhouse"))
		{
			Building b = new Bathhouse(pos, tile);
			model.AddBuilding(b);
			view.UpdateBuilding(b);
		}
		if (currentBuildingName.Equals("Castle"))
		{
			Building b = new Castle(pos, tile);
			model.AddBuilding(b);
			view.UpdateBuilding(b);
		}
		//etc...

	}


	public void HandleClick()
	{
		Vector3Int clickedCell = GetTileCoordinates();

		if (!tilemap.HasTile(clickedCell)) return;

		Tile tile = (Tile)tilemap.GetTile(clickedCell);

		//Check to see if building on tile
		if (model.CheckForBuilding(clickedCell))
		{
			//If yes, do nothing/bring up some UI thing
		}
		else 
		{
			// No building here, call logic to add one 
			MakeBuilding(clickedCell, tile);
		}
		
	}


	//TODO: Shit fucking name
	private Vector3Int GetTileCoordinates()
	{
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3Int clickedCell = tilemap.WorldToCell(mouseWorldPos);
		return clickedCell;
	}	


	public void AdvanceBuildingStates()
	{
		foreach (Building b in model.buildings.Values)
		{
			b.AdvanceState();
			view.UpdateBuilding(b); 
		}
	}

}
