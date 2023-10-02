using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkMapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    private int iteration = 10;
    [SerializeField]
    public int walkLength = 10;
    [SerializeField] 
    public bool startRandomlyEachIteration = true;

    [SerializeField]
    private TilemapVisualiser tilemapVisualiser;

    public void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualiser.clear();
        tilemapVisualiser.paintFloorTiles(floorPositions);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0;i < iteration; i++) 
        {
            var path = ProceduralSpawn.SimpleRandomWalk(currentPosition, walkLength);
            floorPositions.UnionWith(path);
            if(startRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }
}
