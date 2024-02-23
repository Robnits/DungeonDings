using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkMapGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;
    
    private void Awake()
    {
        GenerateDungeon();
    }
    // Einstiegspunkt f�r die prozedurale Generierung
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    // Hauptmethode f�r die Korridor-erste Dungeon-Generierung
    private void CorridorFirstGeneration()
    {
        // Sets zum Speichern von Bodenpositionen, potenziellen Raumpositionen und Raumpositionen
        List<HashSet<Vector2Int>> floorPositions = new();
        List<HashSet<Vector2Int>> potentialRoomPositions = new();
        NewMethod(floorPositions, potentialRoomPositions);
        for (int i = 0; i < startPosition.Count; i++)
        {
            List<List<Vector2Int>> corridors = CreateCorridors(floorPositions[i], potentialRoomPositions[i], i);

            // R�ume in potenziellen Raumpositionen erstellen
            
            HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions[i]);

            // Sackgassen in Korridoren finden
            List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions[i]);

            // Zus�tzliche R�ume an Sackgassen erstellen
            CreateRoomsAtDeadEnds(deadEnds, roomPositions);

            // Bodenpositionen aus Korridoren und R�umen kombinieren
            floorPositions[i].UnionWith(roomPositions);

            for (int j = 0; j < corridors.Count; j++)
            {
                corridors[j] = IncreaseCorridorSizeByOne(corridors[j]);
                floorPositions[i].UnionWith(corridors[j]);
            }

            // Bodenfliesen visualisieren und W�nde erstellen
            tilemapVisualiser.PaintFloorTiles(floorPositions[i]);
            StartCoroutine(prefabSpawner.WaitForSpawn(floorPositions[i]));
            WallGenerator.CreateWalls(floorPositions[i], tilemapVisualiser);
        }
    }

    private void NewMethod(List<HashSet<Vector2Int>> floorPositions, List<HashSet<Vector2Int>> potentialRoomPositions)
    {
        for (int i = 0; i < startPosition.Count; i++)
        {
            _ = startPosition[i];
            floorPositions.Add(new HashSet<Vector2Int>());
            potentialRoomPositions.Add(new HashSet<Vector2Int>());
        }
    }

    // Zus�tzliche R�ume an Sackgassen erstellen
    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (roomFloors.Contains(position) == false)
            {
                // Einen Raum mit Zufallsweg ab der Sackgasse erstellen
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    // Alle Sackgassen im Dungeon finden
    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new();
        foreach (Vector2Int position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                // Anzahl der Bodenpositionen in der Nachbarschaft einer gegebenen Position z�hlen
                if (floorPositions.Contains(position + direction))
                    neighboursCount++;
            }
            // Wenn es nur einen Nachbarn gibt, handelt es sich um eine Sackgasse
            if (neighboursCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }

    // R�ume in einer Teilmenge potenzieller Raumpositionen erstellen
    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        // Zuf�llig eine Teilmenge potenzieller Raumpositionen ausw�hlen
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        // R�ume mit Zufallsweg ab den ausgew�hlten Positionen erstellen
        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    // Initiale Korridore f�r den Dungeon erstellen
    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions, int SpawnNumber)
    {
        List<List<Vector2Int>> corridors = new();

        var currentPosition = startPosition[SpawnNumber];
        potentialRoomPositions.Add(currentPosition);

        // Generate a series of connected corridors
        for (int j = 0; j < corridorCount; j++)
        {
            // Generate a random walk corridor and update the current position
            var corridor = ProceduralSpawn.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[^1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }

        return corridors;
    }

    public List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new();
        Vector2Int previousDirection = Vector2Int.zero;
        for (int i = 1;i < corridorCount;i++)
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i - 1];
            if (previousDirection != Vector2Int.zero &&
                directionFromCell != previousDirection)
            {

                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                    }
                }
                previousDirection = directionFromCell;
            }
            else
            {
                Vector2Int newCorridorTileOffset
                    = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
            }
        }
        return newCorridor;
    }

    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if(direction == Vector2Int.up)
            return Vector2Int.right;
        if(direction == Vector2Int.right)
            return Vector2Int.down;
        if(direction == Vector2Int.down)
            return Vector2Int.left;
        if(direction == Vector2Int.left)
            return Vector2Int.up;
        return Vector2Int.zero;
    }
}