using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralSpawn
{
    // Start is called before the first frame update
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startposition, int walklenght)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startposition);
        var previousposition = startposition;

        for (int i = 0;i < walklenght; i++)
        {
            var newPosition = previousposition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousposition = newPosition;
        }
        return path;
    }
}
public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>()
    {
        new Vector2Int(0, 1), //up
        new Vector2Int(1, 0), //right
        new Vector2Int(0, -1), //down
        new Vector2Int(-1, 0), //left

    };
    public static Vector2Int GetRandomCardinalDirection() 
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}
