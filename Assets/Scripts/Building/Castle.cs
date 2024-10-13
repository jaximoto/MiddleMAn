using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Buildings 
{
public class Castle : Building
{


	public void StaticInit()
	{
		this.name = "Castle";
		this.dims = new Vector3Int(1, 1, 0);
		this.inProgressSpritePath = "Sprites/InProgress";
		this.completeSpritePath = "Sprites/LandTile";
		this.moneyCost = 50;
		GenericStaticInit();
	}


	//This sucks so hard
	public Castle()
	{
		StaticInit();
	}


	public Castle(Vector3Int _location, Tilemap tilemap)
	{
		this.location = _location;
		this.buildCost = 1.0f;

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
