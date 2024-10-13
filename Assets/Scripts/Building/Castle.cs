
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Buildings 
{
public class Castle : Building
{

	public static string inProgressSpritePath = "Sprites/InProgress";
	public static string completeSpritePath = "Sprites/LandTile";

	//This sucks so hard
	public Castle()
	{
		this.name = "Castle";
		this.inProgressSprite = Resources.Load<Sprite>(inProgressSpritePath);
		this.completeSprite = Resources.Load<Sprite>(completeSpritePath);
	}

	public Castle(Vector3Int _location, Tile _tile)
	{
		this.location = _location;
		this.tile = _tile;
		this.name = "Castle";

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

}
