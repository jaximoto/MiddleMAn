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
		this.completeSpritePath = "Sprites/SmallCastle";
		this.moneyCost = 10000;
		this.buildCost = 500f;

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
