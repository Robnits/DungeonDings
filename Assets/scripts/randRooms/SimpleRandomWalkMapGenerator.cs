using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;



// Ein einfacher Dungeon-Generator, der zufällige Wege durchläuft
public class SimpleRandomWalkMapGenerator : AbstractDungeonGenerator
{
    // Parameter für den einfachen zufälligen Weg
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


    // Methode für die Ausführung der prozeduralen Generierung
    protected override void RunProceduralGeneration()
    {
        // Zufälliger Weg ausführen und Bodenpositionen darstellen
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        tilemapVisualiser.clear();
        tilemapVisualiser.paintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualiser);
    }

    // Methode für das Ausführen eines einfachen zufälligen Wegs
    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        // Aktuelle Position für den zufälligen Weg
        var currentPosition = position;
        // Menge für die Bodenpositionen des zufälligen Wegs
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        // Iterationen für den zufälligen Weg durchführen
        for (int i = 0; i < parameters.iterations; i++)
        {
            // Zufälligen Weg erstellen und Bodenpositionen hinzufügen
            var path = ProceduralSpawn.SimpleRandomWalk(currentPosition, parameters.walklenght);
            floorPositions.UnionWith(path);          
            
            // Wenn in jeder Iteration zufällig gestartet werden soll
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