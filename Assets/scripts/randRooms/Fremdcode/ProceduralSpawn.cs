using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Eine statische Klasse f�r prozedurale Erzeugung von Wegen und Korridoren
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////
/// Fremdcode Sunny Valley Studio Folge 1 - 12
/// https://www.youtube.com/watch?v=-QOCX6SVFsk&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v
/// </summary>
public static class ProceduralSpawn
{
    // Einfacher zuf�lliger Weg von einer Startposition aus
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        // Menge f�r den zuf�lligen Weg
        HashSet<Vector2Int> path = new()
        {
            // Startposition hinzuf�gen
            startPosition
        };
        var previousPosition = startPosition;

        // Zuf�lligen Weg erstellen
        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }

    // Zuf�lliger Weg f�r einen Korridor von einer Startposition aus
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        // Liste f�r den Korridor
        List<Vector2Int> corridor = new();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        // Zuf�lligen Korridor erstellen
        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }
}

// Eine statische Klasse f�r 2D-Richtungen
public static class Direction2D
{
    // Liste der kardinalen Richtungen (oben, rechts, unten, links)
    public static List<Vector2Int> cardinalDirectionList = new()
    {
        new Vector2Int(0, 1), // oben
        new Vector2Int(1, 0), // rechtss
        new Vector2Int(0, -1), // unten
        new Vector2Int(-1, 0), // links
    };
    public static List<Vector2Int> diagonalDirectionList = new()
    {
        new Vector2Int(1, 1), // oben rechts
        new Vector2Int(1, -1), // unten rechts
        new Vector2Int(-1, -1), // unten links
        new Vector2Int(-1, 1), // oben links
    };

    // Methode zur Auswahl einer zuf�lligen kardinalen Richtung
    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}
