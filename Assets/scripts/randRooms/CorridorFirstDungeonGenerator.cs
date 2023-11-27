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
    
    private void Start()
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
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

        // R�ume in potenziellen Raumpositionen erstellen
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        // Sackgassen in Korridoren finden
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        // Zus�tzliche R�ume an Sackgassen erstellen
        CreateRoomsAtDeadEnds(deadEnds, roomPositions);

        // Bodenpositionen aus Korridoren und R�umen kombinieren
        floorPositions.UnionWith(roomPositions);

        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorSizeByOne(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }

        // Bodenfliesen visualisieren und W�nde erstellen
        tilemapVisualiser.paintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualiser);
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
        List<Vector2Int> deadEnds = new List<Vector2Int>();
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
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
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
    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        // Eine Serie verbundener Korridore generieren
        for (int i = 0; i < corridorCount; i++)
        {
            // Einen Zufallswegkorridor generieren und die aktuelle Position aktualisieren
            var corridor = ProceduralSpawn.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
        return corridors;
    }

    public List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
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