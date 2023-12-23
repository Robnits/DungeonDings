using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;



// Ein einfacher Dungeon-Generator, der zuf�llige Wege durchl�uft
public class SimpleRandomWalkMapGenerator : AbstractDungeonGenerator
{
    // Parameter f�r den einfachen zuf�lligen Weg
    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;

    public GameObject ratspwaner;
    public GameObject chestspawner;

    [SerializeField]
    [Range(0, 100)]
    private float Spawnpercantage;

    [SerializeField]
    [Range(0, 100)]
    private float spawnChest;


    // Methode f�r die Ausf�hrung der prozeduralen Generierung
    protected override void RunProceduralGeneration()
    {
        // Zuf�lliger Weg ausf�hren und Bodenpositionen darstellen
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        tilemapVisualiser.clear();
        tilemapVisualiser.paintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualiser);
    }

    // Methode f�r das Ausf�hren eines einfachen zuf�lligen Wegs
    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        // Aktuelle Position f�r den zuf�lligen Weg
        var currentPosition = position;
        // Menge f�r die Bodenpositionen des zuf�lligen Wegs
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        // Iterationen f�r den zuf�lligen Weg durchf�hren
        for (int i = 0; i < parameters.iterations; i++)
        {
            // Zuf�lligen Weg erstellen und Bodenpositionen hinzuf�gen
            var path = ProceduralSpawn.SimpleRandomWalk(currentPosition, parameters.walklenght);
            floorPositions.UnionWith(path);          
            
            // Wenn in jeder Iteration zuf�llig gestartet werden soll
            if (parameters.startRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        testspawn(floorPositions, 1);
        testspawn(floorPositions, 2);

        return floorPositions;
    }

    private void testspawn(HashSet<Vector2Int> floorPositions, int whatGetGenerated)
    {
        int hilf;
        foreach (Vector2Int position in floorPositions)
        {
            switch (whatGetGenerated)
            {
                case 1:
                    hilf = Random.Range(0, 100);
                    if (hilf < Spawnpercantage)
                        InstantiatePrefabsThatSpawnOnMap(position, whatGetGenerated);
                    
                    break;
                case 2:
                    hilf = Random.Range(0, 100);
                    if (hilf < spawnChest)
                        InstantiatePrefabsThatSpawnOnMap(position, whatGetGenerated);
                    break;
            }
        }
    }

    private void InstantiatePrefabsThatSpawnOnMap(Vector2Int positions, int whatGetGenerated)
    {
        switch (whatGetGenerated)
        {
            case 1:
                // Instantiate the ratspwaner at the chosen position
                Instantiate(ratspwaner, new Vector3(positions.x + 0.5f, positions.y + 0.5f, 0f), Quaternion.identity);
                break;
            case 2:
                Instantiate(chestspawner, new Vector3(positions.x + 0.5f, positions.y + 0.5f, 0f), Quaternion.identity);
                break;
        }
        
        
    }
}