using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using static Building;

public class BuildingView : MonoBehaviour
{
	public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public void UpdateBuilding(Building b)
	{
		//This is so fuckign stupid
		b.tile.sprite = b.currentSprite;
		tilemap.RefreshTile(b.location);
	}
}
