using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Buildings
{

public class Bathhouse : Building
{

	void StaticInit()
	{
		this.name = "Bathhouse";
		this.dims = new Vector3Int(2, 2, 0);

		this.inProgressTilePath = "Tiles/InProgress";
		this.completeTilePath = "Tiles/Bathhouse";

		this.moneyCost = 100000;
		this.buildCost = 1000f;

		GenericStaticInit();
	}


	//This sucks so hard
	public Bathhouse()
	{
		StaticInit();
	}


	public Bathhouse(Vector3Int _location, Tilemap tilemap)
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
