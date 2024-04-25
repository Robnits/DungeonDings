using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////
/// Fremdcode Sunny Valley Studio Folge 1 - 12
/// https://www.youtube.com/watch?v=-QOCX6SVFsk&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v
/// </summary>

// Ein einfacher Dungeon-Generator, der zuf�llige Wege durchl�uft
public class SimpleRandomWalkMapGenerator : AbstractDungeonGenerator
{
    // Parameter f�r den einfachen zuf�lligen Weg
    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;

    public PrefabSpawner prefabSpawner;

    // Methode f�r die Ausf�hrung der prozeduralen Generierung
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = new();
        // Zuf�lliger Weg ausf�hren und Bodenpositionen darstellen
        for (int i = 0; i < startPosition.Count(); i++)
        {
            floorPositions = RunRandomWalk(randomWalkParameters, startPosition[i]);
        }
        tilemapVisualiser.Clear();
        prefabSpawner.WhatshouldSpawn(tilemapVisualiser.PaintFloorTiles(floorPositions)); 
        WallGenerator.CreateWalls(floorPositions, tilemapVisualiser);
    }

    // Methode f�r das Ausf�hren eines einfachen zuf�lligen Wegs
    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        // Aktuelle Position f�r den zuf�lligen Weg
        var currentPosition = position;
        // Menge f�r die Bodenpositionen des zuf�lligen Wegs
        HashSet<Vector2Int> floorPositions = new();

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
        
        

        return floorPositions;
    }
}