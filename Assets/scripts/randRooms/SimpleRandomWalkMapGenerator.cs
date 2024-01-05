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

    public PrefabSpawner prefabSpawner;

    // Methode für die Ausführung der prozeduralen Generierung
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        // Zufälliger Weg ausführen und Bodenpositionen darstellen
        for (int i = 0; i < startPosition.Count(); i++)
        {
            floorPositions = RunRandomWalk(randomWalkParameters, startPosition[i]);
        }
        tilemapVisualiser.Clear();
        tilemapVisualiser.PaintFloorTiles(floorPositions); 
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
        
        

        return floorPositions;
    }
}