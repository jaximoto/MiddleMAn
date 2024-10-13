using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using static BuildingView;
using static BuildingModel;

using static Bathhouse;//TODO make it so we dont have to include all building types

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
		if (Input.GetMouseButtonDown(0)) //TODO Need some way to make it so the fucking player cant just spam click like a fucking monkey
		{
			//TODO: How to get building type from user
			//TODO: Differnet typyes of builigngs
			HandleClick();
		}
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
			Bathhouse b = new Bathhouse(clickedCell, tile);
			model.AddBuilding(b);

			// Update tile (nice fucking comment)
			view.UpdateBuilding(b);
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
