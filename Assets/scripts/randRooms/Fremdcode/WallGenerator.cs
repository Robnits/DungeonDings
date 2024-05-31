using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Eine statische Klasse f�r die Erzeugung von W�nden basierend auf Bodenpositionen
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////
/// Fremdcode Sunny Valley Studio Folge 1 - 12
/// https://www.youtube.com/watch?v=-QOCX6SVFsk&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v
/// </summary>

public static class WallGenerator
{
    // Methode zum Erzeugen von W�nden auf der TilemapVisualiser anhand von Bodenpositionen
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualiser tilemapVisualiser)
    {
        
        // Wandpositionen finden und einfache W�nde darstellen
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionList);
        var cornerWallPositions = FindCornerWalls(floorPositions, Direction2D.diagonalDirectionList);

        basicWallPositions.UnionWith(cornerWallPositions);

        foreach (var position in basicWallPositions)
        {
            tilemapVisualiser.PaintSingleBasicWall(position);
        }
    }

    // Methode zum Finden von Wandpositionen in bestimmten Richtungen
    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        // Menge f�r Wandpositionen
        HashSet<Vector2Int> wallPositions = new();

        // F�r jede Bodenposition pr�fen, ob sie Nachbarn in bestimmten Richtungen hat
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


    private static HashSet<Vector2Int> FindCornerWalls(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {

        HashSet<Vector2Int> wallPositions = new();
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
