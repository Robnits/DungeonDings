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
    public void paintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        paintTiles(floorPositions, floorTilemap, floorTile);
    }


    // Hilfsmethode zum Darstellen von Tiles an den angegebenen Positionen auf der Tilemap
    private void paintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        
        foreach (var position in positions)
        {
            paintSingleTile(tilemap, tile, position);
        }
    }

    // Hilfsmethode zum Darstellen eines einzelnen Tiles an einer Position auf der Tilemap
    private void paintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    // Methode zum Zurücksetzen der Tilemaps
    public void clear()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chests");

        List<GameObject> gameObjects = new List<GameObject>();
        gameObjects.Add(spawners[0]);
        gameObjects.Add(chests[0]);


        // Destroy each spawner
        foreach (GameObject gameObject in gameObjects)
        {
            DestroyImmediate(spawner);
        }

        // Clear tilemaps
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    // Methode zum Darstellen eines einzelnen Basis-Wall-Tiles an einer Position auf der Tilemap
    internal void PaintSingleBasicWall(Vector2Int position)
    {
        paintSingleTile(wallTilemap, wallTop, position);
    }
}
