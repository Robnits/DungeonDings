using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////
/// Fremdcode Sunny Valley Studio Folge 1 - 12
/// https://www.youtube.com/watch?v=-QOCX6SVFsk&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v
/// </summary>

// Klasse zur Visualisierung von Tiles auf Tilemaps
public class TilemapVisualiser : MonoBehaviour
{
    // Referenzen auf die Tilemaps und Tilesets
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private List<SOTiles> tilesSO;

    private const int WallRandomRangeMax = 1001;

    private int randomBiom;

    private void Awake()
    {
        randomBiom = Random.Range(0, 2);
    }

    // Methode zum Darstellen von Boden-Tiles an den angegebenen Positionen
    public int PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        foreach (var position in floorPositions)
        {
            int randomValue = Random.Range(0, WallRandomRangeMax);

            if (randomValue >= 100 && tilesSO[randomBiom].raretiles.Count() > 1)
            {
                PaintSingleTile(position, floorTilemap, tilesSO[randomBiom].raretiles[randomValue % tilesSO[randomBiom].raretiles.Count()]);
                Debug.Log("RareTileSpawnded" + position);
            }
            else
                PaintSingleTile(position, floorTilemap, tilesSO[randomBiom].tiles[randomValue % tilesSO[randomBiom].tiles.Count()]);
        }
        return randomBiom;
    }
    private void PaintSingleTile(Vector2Int position, Tilemap tilemap, TileBase tile)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        GameObject parentObject = GameObject.FindGameObjectWithTag("shouldGetDestroyedAfterRebuild");
        GameObject[] childObjects;

        childObjects = new GameObject[parentObject.transform.childCount];
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            childObjects[i] = parentObject.transform.GetChild(i).gameObject;
        }

        foreach (GameObject todelte in childObjects)
        {
            DestroyImmediate(todelte);
        }

        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    // Methode zum Darstellen eines einzelnen Basis-Wall-Tiles an einer Position auf der Tilemap
    internal void PaintSingleBasicWall(Vector2Int position)
    {
        int whichWall = Random.Range(1, tilesSO[randomBiom].wall.Count());
        PaintSingleTile(position, wallTilemap, tilesSO[randomBiom].wall[whichWall - 1]);
    }
}
