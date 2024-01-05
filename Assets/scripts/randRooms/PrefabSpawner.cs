using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PrefabSpawner : MonoBehaviour
{

    public GameObject ratspwaner;
    public GameObject chestspawner;
    public GameObject stairsDown;
    public GameObject stairsUp;
    public GameObject parentGameobject;
    private int stairnumber = 0;

    [SerializeField]
    private BalancingSystemSO balancingSO;


    [SerializeField]
    [Range(0, 100)]
    private float SpawnerSpawnPercantage;

    [SerializeField]
    [Range(0, 100)]
    private float ChestSpawnPercantage;

    private void Awake()
    {
        SpawnerSpawnPercantage = balancingSO.SpawnSpawner;
        ChestSpawnPercantage = balancingSO.SpawnChest;
    }
    public enum WhatGetSpawned
    {
        RatSpawner,
        Chest,
        Exit
    }

    public void SpawnExit(HashSet<Vector2Int> floorPositions)
    {
        
        Instantiate(stairsDown, new Vector3(floorPositions.Last<Vector2Int>().x + 0.5f, floorPositions.Last<Vector2Int>().y + 0.5f, 0f), Quaternion.identity);
        foreach (var item in GameObject.FindGameObjectsWithTag("DOWN"))
            item.GetComponent<Stairs>().WhatIsMyNumber(stairnumber);
        
        stairnumber++;
        Instantiate(stairsUp, new Vector3(floorPositions.First<Vector2Int>().x + 0.5f, floorPositions.First<Vector2Int>().y + 0.5f, 0f), Quaternion.identity);

        foreach (var item in GameObject.FindGameObjectsWithTag("UP"))
            item.GetComponent<Stairs>().WhatIsMyNumber(stairnumber);

        stairnumber++;
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
                if (hilf < SpawnerSpawnPercantage)
                {
                    InstantiatePrefabsThatSpawnOnMap(position, WhatGetSpawned.RatSpawner);
                    SpawnedPositions.Add(position);
                }

                hilf = Random.Range(0, 100);
                // Check again after spawning the RatSpawner
                if (hilf < ChestSpawnPercantage)
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
                Instantiate(ratspwaner, new Vector3(position.x + 0.5f, position.y + 0.5f, 0f), Quaternion.identity, parentGameobject.transform);
                break;
            case WhatGetSpawned.Chest:
                Instantiate(chestspawner, new Vector3(position.x + 0.5f, position.y + 0.5f, 0f), Quaternion.identity, parentGameobject.transform);
                break;
        }
    }
}
