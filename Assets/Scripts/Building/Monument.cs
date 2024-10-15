using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Buildings 
{
public class Monument : Building
{


	public void StaticInit()
	{
		this.name = "Monument";
		this.dims = new Vector3Int(3, 3, 0);

		this.inProgressTilePath = "Tiles/InProgress";
		this.completeTilePath = "Tiles/Monument";

		this.moneyCost = 30000;
		this.buildCost = 5000f;

		GenericStaticInit();
	}


	//This sucks so hard
	public Monument()
	{
		StaticInit();
	}


	public Monument(Vector3Int _location, Tilemap tilemap)
	{
		this.location = _location;

		StaticInit();
		GenericInit();

		foreach (Vector3Int pos in residentCoordinates)
		{
			this.tiles.Add((Tile)tilemap.GetTile(pos));
		}

	}


	public override void DayAction()
	{
		//Generate money or something
	}

}

}
