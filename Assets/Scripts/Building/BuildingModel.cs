using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils;
using Buildings;


public class BuildingModel
{
	public string[] buildingNames = {"Bathhouse", "Castle"};
	public CircularList<string> buildingOptions;

	public Dictionary<Vector3Int, Building> buildings;
	public Dictionary<string, Building> buildingsMap;
	public HashSet<Vector3Int> occupiedTiles;

	public Building dummyBuilding;
	public Bathhouse dummyBathhouse; //This sucks
	public Castle dummyCastle;

	public string equippedBuildingName;
	
	public Vector3Int lastHighlightedCell;
	public Color lastCellColor;


	public BuildingModel()
	{
		this.buildings = new Dictionary<Vector3Int, Building>();
		this.buildingOptions = new CircularList<string>(buildingNames);
		this.occupiedTiles = new HashSet<Vector3Int>();

		this.dummyBathhouse = new Bathhouse();
		this.dummyCastle = new Castle();

		this.buildingsMap = new()
		{
			{ buildingNames[0], this.dummyBathhouse},
			{ buildingNames[1], this.dummyCastle}
		};

		this.lastHighlightedCell = new Vector3Int(-100000000, -10000000, 0);

	}


	public void AddBuilding(Building b)
	{
		foreach(Vector3Int pos in b.residentCoordinates)
		{
			occupiedTiles.Add(pos);
			buildings.Add(pos, b);
		}
	}


	public void RemoveBuilding(Building b)
	{
		buildings.Remove(b.location);

		foreach(Vector3Int pos in b.residentCoordinates)
		{
			occupiedTiles.Remove(pos);
		}
	}


	public bool CheckForBuilding(List<Vector3Int> coords)
	{
		foreach (Vector3Int coord in coords)
		{
			if (occupiedTiles.Contains(coord))
				return true;
		}
		return false;
	}


	public void EquipBuilding()
	{
		equippedBuildingName = buildingOptions.Current();
		dummyBuilding = buildingsMap[equippedBuildingName];
	}

}
