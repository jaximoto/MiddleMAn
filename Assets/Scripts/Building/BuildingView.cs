using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

using Buildings;

public class BuildingView : MonoBehaviour
{
	public Tilemap tilemap;
	public TextMeshProUGUI equippedBuildingName; 
	public GameObject equippedBuildingSprite;

	public SpriteRenderer equippedBuildingRenderer;


    // Start is called before the first frame update
    void Start()
    {
		equippedBuildingRenderer = equippedBuildingSprite.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public void UpdateBuilding(Building b)
	{
		//This is so fuckign stupid
		for (int i=0; i<b.tiles.Count; i++)
		{
			tilemap.SetTile(b.residentCoordinates[i], b.currentTiles[i]);
			tilemap.RefreshTile(b.residentCoordinates[i]);
		}
	}


	public void UpdateEquippedBuilding(Building b)
	{
		equippedBuildingName.text = b.name;
		equippedBuildingRenderer.sprite = b.completeTile.sprite;
	}

	
	public void HighlightTile(Vector3Int cell, Vector3Int lastHighlightedCell, Color lastColor)
	{
		if (lastHighlightedCell == cell)
			return;

		tilemap.SetTileFlags(cell, TileFlags.None);
		tilemap.SetColor(cell, Color.blue);

		tilemap.SetTileFlags(lastHighlightedCell, TileFlags.None);
		tilemap.SetColor(lastHighlightedCell, lastColor);
	}
}
