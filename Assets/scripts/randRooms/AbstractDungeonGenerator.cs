using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Eine abstrakte Basisklasse f�r Dungeon-Generatoren
public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    // Referenz auf den TilemapVisualiser zur Darstellung des Dungeons
    [SerializeField]
    protected TilemapVisualiser tilemapVisualiser = null;

    // Startposition f�r die Dungeon-Generierung
    [SerializeField]
    protected List<Vector2Int> startPosition;


    // Methode zum Generieren des Dungeons
    public void GenerateDungeon()
    {
        // TilemapVisualiser zur�cksetzen und die Generierung starten
        tilemapVisualiser.Clear();
        RunProceduralGeneration();
    }

    // Abstrakte Methode, die in abgeleiteten Klassen implementiert werden muss
    protected abstract void RunProceduralGeneration();
}