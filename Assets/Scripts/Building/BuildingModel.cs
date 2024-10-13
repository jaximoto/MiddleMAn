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

	public Building dummyBuilding;
	public Bathhouse dummyBathhouse; //This sucks
	public Castle dummyCastle;


	public BuildingModel()
	{
		this.buildings = new Dictionary<Vector3Int, Building>();
		this.buildingOptions = new CircularList<string>(buildingNames);

		this.dummyBathhouse = new Bathhouse();
		this.dummyCastle = new Castle();
	}

	public void AddBuilding(Building b)
	{
		//TODO: Need to interact with user stats like money
		buildings.Add(b.location, b);
	}

	public void RemoveBuilding(Building b)
	{
		buildings.Remove(b.location);
	}

	//TODO shit name
	public bool CheckForBuilding(Vector3Int pos)
	{
		return buildings.ContainsKey(pos);
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
