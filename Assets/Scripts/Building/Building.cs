using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Buildings
{
public abstract class Building
{
	public enum Status
	{
		inProgress,
		done
	}

	public Status status;

	public string name;

	public float cost;
	public float buildTime;

	public abstract void DayAction();
	public abstract void AdvanceState();//shit name

	public Vector3Int location;
	public Vector3Int dims;

	public Sprite inProgressSprite;
	public Sprite completeSprite;
	public Sprite currentSprite;

	public Tile tile;


}
}
