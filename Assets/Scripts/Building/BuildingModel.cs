using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Building;


public class BuildingModel
{

	public Dictionary<Vector3Int, Building> buildings;

	public BuildingModel()
	{
		this.buildings = new Dictionary<Vector3Int, Building>();
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
}
