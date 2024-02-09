using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<SOTiles> tilesSO;

    private const int WallRandomRangeMax = 1001;

    private int randomBiom;

    private void Awake()
    {
        randomBiom = Random.Range(0, 3);
    }


    // Methode zum Darstellen von Boden-Tiles an den angegebenen Positionen
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
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
        
    }
    private void PaintSingleTile(Vector2Int position, Tilemap tilemap, TileBase tile)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    // Methode zum Zurücksetzen der Tilemaps
    public void Clear()
    {

        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        GameObject[] downSpawners = GameObject.FindGameObjectsWithTag("DOWN");
        GameObject[] upSpawners = GameObject.FindGameObjectsWithTag("UP");
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] saeule = GameObject.FindGameObjectsWithTag("shouldGetDestroyedAfterRebuild");


        GameObject[] allSpawns = spawners.Concat(downSpawners).Concat(upSpawners).Concat(spawners).Concat(chests).Concat(enemys).Concat(saeule).ToArray();

        foreach (GameObject spawner in allSpawns)
        {
            DestroyImmediate(spawner);
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
