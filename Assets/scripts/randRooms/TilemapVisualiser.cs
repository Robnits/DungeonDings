using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Klasse zur Visualisierung von Tiles auf Tilemaps
public class TilemapVisualiser : MonoBehaviour
{
    // Referenzen auf die Tilemaps und Tilesets
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop;

    // Methode zum Darstellen von Boden-Tiles an den angegebenen Positionen
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }


    // Hilfsmethode zum Darstellen von Tiles an den angegebenen Positionen auf der Tilemap
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    // Hilfsmethode zum Darstellen eines einzelnen Tiles an einer Position auf der Tilemap
    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    // Methode zum Zurücksetzen der Tilemaps
    public void Clear()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
        GameObject[] exits = GameObject.FindGameObjectsWithTag("Exit");


        // Destroy each spawner
        foreach (GameObject spawner in spawners)
        {
            DestroyImmediate(spawner);
        }
        foreach (GameObject chest in chests)
        {
            DestroyImmediate(chest);
        }
        foreach (var exit in exits)
        {
            DestroyImmediate(exit);
        }


        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    // Methode zum Darstellen eines einzelnen Basis-Wall-Tiles an einer Position auf der Tilemap
    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTop, position);
    }
}
