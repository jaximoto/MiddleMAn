using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Buildings 
{
public class House : Building
{


	public void StaticInit()
	{
		this.name = "House";
		this.dims = new Vector3Int(1, 1, 0);

		this.inProgressTilePath = "Tiles/InProgress";
		this.completeTilePath = "Tiles/House";

		this.moneyCost = 1000;
		this.buildCost = 100f;

		GenericStaticInit();
	}


	//This sucks so hard
	public House()
	{
		StaticInit();
	}


	public House(Vector3Int _location, Tilemap tilemap)
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
