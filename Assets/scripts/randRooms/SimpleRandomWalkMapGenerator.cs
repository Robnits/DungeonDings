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

    private int a;

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

            a++;
            if (a == 30)
            {
                a = 0;
                InstantiateRatspwaner(floorPositions);
            }
            
            
            // Wenn in jeder Iteration zuf�llig gestartet werden soll
            if (parameters.startRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }

    private void InstantiateRatspwaner(HashSet<Vector2Int> positions)
    {
        if (positions.Count > 0)
        {
            // Choose a random index
            int randomIndex = Random.Range(0, positions.Count);

            // Get the corresponding position
            Vector2Int randomPosition = positions.ElementAt(randomIndex);

            // Instantiate the ratspwaner at the chosen position
            Instantiate(ratspwaner, new Vector3(randomPosition.x, randomPosition.y, 0f), Quaternion.identity);
        }
    }
}