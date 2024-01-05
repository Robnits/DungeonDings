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
    protected List<Vector2Int> startPosition;


    // Methode zum Generieren des Dungeons
    public void GenerateDungeon()
    {
        // TilemapVisualiser zurücksetzen und die Generierung starten
        tilemapVisualiser.Clear();
        RunProceduralGeneration();
    }

    // Abstrakte Methode, die in abgeleiteten Klassen implementiert werden muss
    protected abstract void RunProceduralGeneration();
}