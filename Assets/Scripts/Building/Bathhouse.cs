using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Buildings
{

public class Bathhouse : Building
{

	//TODO Lots needs to  be moved to base class
	public static string inProgressSpritePath = "Sprites/InProgress";
	public static string completeSpritePath = "Sprites/SmallCastle";

	//This sucks so hard
	public Bathhouse()
	{
		this.name = "Bathhouse";
		this.inProgressSprite = Resources.Load<Sprite>(inProgressSpritePath);
		this.completeSprite = Resources.Load<Sprite>(completeSpritePath);
	}

	public Bathhouse(Vector3Int _location, Tile _tile)
	{
		this.location = _location;
		this.tile = _tile;
		this.name = "Bathhouse";

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