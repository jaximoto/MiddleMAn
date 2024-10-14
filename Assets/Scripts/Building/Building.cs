using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using static StatsManager;


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

	public int moneyCost;
	public float buildCost;
	public float buildProgress;

	public abstract void DayAction();

	public Vector3Int location;
	public Vector3Int dims;

	public string inProgressTilePath;
	public string completeTilePath;

	public Tile completeTile;
	public Tile inProgressTile;

	public List<Tile> tiles;

	//Matrix of coordinates this building occupies row major
	//This should proabbly be one list of tuples but showuld be fine
	//This is fucking inexcusable
	public List<Vector3Int> residentCoordinates;
	public List<Tile> completeTiles;
	public List<Tile> currentTiles;
	public List<Tile> inProgressTiles;

	public void SetResidentCoordinates()
	{
		residentCoordinates = new List<Vector3Int>();
		for (int i=0; i<dims.y; i++)
		{
			for (int j=0; j<dims.x; j++)
			{
				residentCoordinates.Add(new Vector3Int(location.x + j, location.y - i, 0));
			}
		}

	}


	public List<Vector3Int> EnumerateCoordinates(Vector3Int pos)
	{
		List<Vector3Int> result = new List<Vector3Int>();
		for (int i=0; i<dims.y; i++)
		{
			for (int j=0; j<dims.x; j++)
			{
				result.Add(new Vector3Int(pos.x + j, pos.y - i, 0));
			}
		}
		return result;
	}


	public void GenericStaticInit()
	{
		this.inProgressTiles = new List<Tile>();
		this.completeTiles = new List<Tile>();

		this.completeTile = Resources.Load<Tile>(completeTilePath+"Entire");
		this.inProgressTile = Resources.Load<Tile>(inProgressTilePath);

	}


	public void GenericInit()
	{
		this.tiles = new List<Tile>();

		SetResidentCoordinates();

		if (residentCoordinates.Count == 1)
		{
			this.completeTiles.Add(this.completeTile);
		}
		else 
		{
			for (int i=0; i<residentCoordinates.Count; i++)
			{
				this.completeTiles.Add(Resources.Load<Tile>(completeTilePath+$"_{i}"));
			}
		}

		for (int i=0; i<residentCoordinates.Count; i++)
		{
			this.inProgressTiles.Add(this.inProgressTile);
		}

		this.currentTiles = this.inProgressTiles;
		this.status = Status.inProgress;
		this.buildProgress = 0.0f;
	}


	public void AdvanceState(float progress)
	{
		buildProgress += progress;

		if (buildProgress >= buildCost)
		{
			this.status = Status.done;
		}

		if (this.status == Status.done)
			this.currentTiles = this.completeTiles;

	}

}
}
