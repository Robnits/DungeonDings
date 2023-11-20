using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Eine statische Klasse für die Erzeugung von Wänden basierend auf Bodenpositionen
public static class WallGenerator
{
    // Methode zum Erzeugen von Wänden auf der TilemapVisualiser anhand von Bodenpositionen
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualiser tilemapVisualiser)
    {
        // Wandpositionen finden und einfache Wände darstellen
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionList);
        foreach (var position in basicWallPositions)
        {
            tilemapVisualiser.PaintSingleBasicWall(position);
        }
    }

    // Methode zum Finden von Wandpositionen in bestimmten Richtungen
    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        // Menge für Wandpositionen
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        // Für jede Bodenposition prüfen, ob sie Nachbarn in bestimmten Richtungen hat
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;
                // Wenn die Nachbarposition keine Bodenposition ist, handelt es sich um eine Wandposition
                if (floorPositions.Contains(neighbourPosition) == false)
                    wallPositions.Add(neighbourPosition);
            }
        }
        return wallPositions;
    }
}
