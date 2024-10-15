using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Buildings
{

public class BuildingFactory
{
	public static Building MakeBuilding(string name, Vector3Int pos, Tilemap tilemap)
	{
		if (name.Equals("Bathhouse"))
		{
			return new Bathhouse(pos, tilemap);
		}
		else if (name.Equals("Castle"))
		{
			return new Castle(pos, tilemap);
		}
		else if (name.Equals("Monument"))
		{
			return new Monument(pos, tilemap);
		}
		else
		{
			return new Bathhouse(pos, tilemap);
		}

	}
}

}
