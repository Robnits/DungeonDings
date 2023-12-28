using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{

    public GameObject ratspwaner;
    public GameObject chestspawner;
    public GameObject exit;


    [SerializeField]
    [Range(0, 100)]
    private float spawnpercantage;

    [SerializeField]
    [Range(0, 100)]
    private float spawnChest;

    public enum WhatGetSpawned
    {
        RatSpawner,
        Chest,
        Exit
    }
    public void SpawnExit(Vector2Int lastFloorPositions)
    {
        InstantiatePrefabsThatSpawnOnMap(lastFloorPositions, WhatGetSpawned.Exit);
    }

    public void SpawnObjects(HashSet<Vector2Int> floorPositions)
    {
        HashSet<Vector2Int> SpawnedPositions = new HashSet<Vector2Int>();
        foreach (Vector2Int position in floorPositions)
        {
            int hilf = Random.Range(0, 100);

            // Check if the position is already occupied in SpawnedPositions
            if (!SpawnedPositions.Contains(position))
            {
                if (hilf < spawnpercantage)
                {
                    InstantiatePrefabsThatSpawnOnMap(position, WhatGetSpawned.RatSpawner);
                    SpawnedPositions.Add(position);
                }

                hilf = Random.Range(0, 100);
                // Check again after spawning the RatSpawner
                if (hilf < spawnChest)
                {
                    InstantiatePrefabsThatSpawnOnMap(position, WhatGetSpawned.Chest);
                    SpawnedPositions.Add(position);
                }
            }
        }
    }

    private void InstantiatePrefabsThatSpawnOnMap(Vector2Int position, WhatGetSpawned whatGetGenerated)
    {
        switch (whatGetGenerated)
        {
            case WhatGetSpawned.RatSpawner:
                // Instantiate the ratspawner at the chosen position
                Instantiate(ratspwaner, new Vector3(position.x + 0.5f, position.y + 0.5f, 0f), Quaternion.identity);
                break;
            case WhatGetSpawned.Chest:
                Instantiate(chestspawner, new Vector3(position.x + 0.5f, position.y + 0.5f, 0f), Quaternion.identity);
                break;
            case WhatGetSpawned.Exit:
                DestroyImmediate(GameObject.FindGameObjectWithTag("Exit"));
                Instantiate(exit, new Vector3(position.x + 0.5f, position.y + 0.5f, 0f), Quaternion.identity);
                break;

        }
    }
}
