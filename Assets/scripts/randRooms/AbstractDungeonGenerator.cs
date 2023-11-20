using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Eine abstrakte Basisklasse für Dungeon-Generatoren
public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    // Referenz auf den TilemapVisualiser zur Darstellung des Dungeons
    [SerializeField]
    protected TilemapVisualiser tilemapVisualiser = null;

    // Startposition für die Dungeon-Generierung
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    // Methode zum Generieren des Dungeons
    public void GenerateDungeon()
    {
        // TilemapVisualiser zurücksetzen und die Generierung starten
        tilemapVisualiser.clear();
        RunProceduralGeneration();
    }

    // Abstrakte Methode, die in abgeleiteten Klassen implementiert werden muss
    protected abstract void RunProceduralGeneration();
}