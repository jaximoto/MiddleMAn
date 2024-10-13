using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Buildings
{

public class Bathhouse : Building
{

	//TODO Lots needs to  be moved to base class
	void StaticInit()
	{
		this.name = "Bathhouse";
		this.dims = new Vector3Int(2, 2, 0);
		this.inProgressSpritePath = "Sprites/InProgress";
		this.completeSpritePath = "Sprites/SmallCastle";
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
		this.buildCost = 1.0f;

		StaticInit();
		GenericInit();

		foreach (Vector3Int pos in residentCoordinates)
		{
			Debug.Log(pos);
			this.tiles.Add((Tile)tilemap.GetTile(pos));
		}

	}


	public override void DayAction()
	{
		//Generate money or something
	}


}

}
