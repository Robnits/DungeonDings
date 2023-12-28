using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Eine statische Klasse für prozedurale Erzeugung von Wegen und Korridoren
public static class ProceduralSpawn
{
    // Einfacher zufälliger Weg von einer Startposition aus
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        // Menge für den zufälligen Weg
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        // Startposition hinzufügen
        path.Add(startPosition);
        var previousPosition = startPosition;

        // Zufälligen Weg erstellen
        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }

    // Zufälliger Weg für einen Korridor von einer Startposition aus
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        // Liste für den Korridor
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        // Zufälligen Korridor erstellen
        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }
}

// Eine statische Klasse für 2D-Richtungen
public static class Direction2D
{
    // Liste der kardinalen Richtungen (oben, rechts, unten, links)
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>()
    {
        new Vector2Int(0, 1), // oben
        new Vector2Int(1, 0), // rechtss
        new Vector2Int(0, -1), // unten
        new Vector2Int(-1, 0), // links
    };

    // Methode zur Auswahl einer zufälligen kardinalen Richtung
    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}
