using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


// Klasse zur Visualisierung von Tiles auf Tilemaps
public class TilemapVisualiser : MonoBehaviour
{
    // Referenzen auf die Tilemaps und Tilesets
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile1, floorTile2, floorTile3, floorTile4, floorTile5, floorTile6, floorTile7, floorTile8, floorTile9, floorTile10, floorTile11, wallTop;

    // Methode zum Darstellen von Boden-Tiles an den angegebenen Positionen
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        foreach (var position in floorPositions)
        {
            int randint = Random.Range(0, 1000);
            
            if (randint == 0)
                PaintSingleTile(position, floorTilemap, floorTile11);
            if (randint == 1)
                PaintSingleTile(position, floorTilemap, floorTile10);
            else
            {
                randint %= 9;
                switch (randint)
                {
                    case 0:
                        PaintSingleTile(position, floorTilemap, floorTile1);
                        break;
                    case 1:
                        PaintSingleTile(position, floorTilemap, floorTile2);
                        break;
                    case 2:
                        PaintSingleTile(position, floorTilemap, floorTile3);
                        break;
                    case 3:
                        PaintSingleTile(position, floorTilemap, floorTile4);
                        break;
                    case 4:
                        PaintSingleTile(position, floorTilemap, floorTile5);
                        break;
                    case 5:
                        PaintSingleTile(position, floorTilemap, floorTile6);
                        break;
                    case 6:
                        PaintSingleTile(position, floorTilemap, floorTile7);
                        break;
                    case 7:
                        PaintSingleTile(position, floorTilemap, floorTile8);
                        break;
                    case 8:
                        PaintSingleTile(position, floorTilemap, floorTile9);
                        break;
                }
            }
        }
    }

    // Hilfsmethode zum Darstellen eines einzelnen Tiles an einer Position auf der Tilemap
    private void PaintSingleTile(Vector2Int position, Tilemap tilemap, TileBase tile)
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
        PaintSingleTile(position, wallTilemap, wallTop);
    }
}
