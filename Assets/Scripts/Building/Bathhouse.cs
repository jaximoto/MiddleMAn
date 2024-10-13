using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using static Building;



public class Bathhouse : Building
{

	public static string inProgressSpritePath = "Sprites/InProgress";
	public static string completeSpritePath = "Sprites/SmallCastle";

	public Bathhouse(Vector3Int _location, Tile _tile)
	{
		this.location = _location;
		this.tile = _tile;

		//TODO Move all this to base class
		this.inProgressSprite = Resources.Load<Sprite>(inProgressSpritePath);
		this.completeSprite = Resources.Load<Sprite>(completeSpritePath);

		this.currentSprite = this.inProgressSprite;

	}


	public override void DayAction()
	{
		//Generate money or something
	}


	public override void AdvanceState()
	{
		// I should be arrested for this
		if (this.currentSprite == this.inProgressSprite)
			this.currentSprite = this.completeSprite;
	}


}
