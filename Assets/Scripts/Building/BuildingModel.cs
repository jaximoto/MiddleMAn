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
	}

	public void AddBuilding(Building b)
	{
		//TODO: Need to interact with user stats like money
		buildings.Add(b.location, b);

		foreach(Vector3Int pos in b.residentCoordinates)
		{
			occupiedTiles.Add(pos);
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

	//TODO shit name
	public bool CheckForBuilding(Vector3Int pos)
	{
		return occupiedTiles.Contains(pos);
	}

	public void EquipBuilding()
	{
		Debug.Log(buildingOptions.Current());
		if (buildingOptions.Current().Equals("Bathhouse"))
		{
			dummyBuilding = dummyBathhouse;
		} 
		else if (buildingOptions.Current().Equals("Castle"))
		{
			dummyBuilding = dummyCastle;
		} 
	}

}
