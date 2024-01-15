using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PrefabSpawner : MonoBehaviour
{

    
    
    public GameObject stairsDown;
    public GameObject stairsUp;
    public GameObject parentGameobject;
    private int stairnumber = 0;
    private readonly List<Vector2Int> SpawnedPositions = new();
    private bool dontSpawnFirstStair = true;

    [SerializeField]
    private BalancingSystemSO easy;
    [SerializeField]
    private BalancingSystemSO middle;
    [SerializeField]
    private BalancingSystemSO hard;

    public GameObject ratspwaner;
    [SerializeField]
    [Range(0, 100)]
    private float SpawnerSpawnPercantage;

    public GameObject chests;
    [SerializeField]
    [Range(0, 100)]
    private float ChestSpawnPercantage;

    public GameObject devils;
    [SerializeField]
    [Range(0, 100)]
    private float devilSpawnPercantage;

    [SerializeField]
    private GameObject Saeule;

    private void Difficulty(BalancingSystemSO difficult)
    {
        SpawnerSpawnPercantage = difficult.spawnSpawner;
        ChestSpawnPercantage = difficult.spawnChest;
        devilSpawnPercantage = difficult.devilSpawnrate;
    }

    public enum WhatGetSpawned
    {
        RatSpawner,
        Chest,
        Exit,
        Devil,
        Down,
        Up,
        Saeule
    }

    public IEnumerator WaitForSpawn(HashSet<Vector2Int> pos)
    {
        yield return new WaitForSeconds(0.2f);
        SpawnExit(pos);
    }
    public void SpawnExit(HashSet<Vector2Int> floorPositions)
    {
        Difficulty(easy);
        if (dontSpawnFirstStair)
        {
            InstantiatePrefabsThatSpawnOnMap(new Vector2Int(40, -90), WhatGetSpawned.Up);
            foreach (var item in GameObject.FindGameObjectsWithTag("UP"))
                item.GetComponent<Stairs>().WhatIsMyNumber(stairnumber);
            dontSpawnFirstStair = false;
        }
        else
        {
            InstantiatePrefabsThatSpawnOnMap(floorPositions.First(), WhatGetSpawned.Up);
            foreach (var item in GameObject.FindGameObjectsWithTag("UP"))
                item.GetComponent<Stairs>().WhatIsMyNumber(stairnumber);
        }
        stairnumber++;
        InstantiatePrefabsThatSpawnOnMap(floorPositions.Last(), WhatGetSpawned.Down);
        foreach (var item in GameObject.FindGameObjectsWithTag("DOWN"))
            item.GetComponent<Stairs>().WhatIsMyNumber(stairnumber);

        Vector2Int middlePos = floorPositions.ElementAt(floorPositions.Count / 2);
        InstantiatePrefabsThatSpawnOnMap(middlePos, WhatGetSpawned.Saeule);

        stairnumber++;
        SpawnedPositions.Add(floorPositions.First());
        SpawnedPositions.Add(floorPositions.Last());
        SpawnObjects(floorPositions);
    }

    public void SpawnObjects(HashSet<Vector2Int> floorPositions)
    {
        
        foreach (Vector2Int position in floorPositions)
        {
            float hilf = Random.Range(0, 100);

            // Check if the position is already occupied in SpawnedPositions
            if (hilf < SpawnerSpawnPercantage && !SpawnedPositions.Contains(position))
            {
                InstantiatePrefabsThatSpawnOnMap(position, WhatGetSpawned.RatSpawner);
                SpawnedPositions.Add(position);
            }

            hilf = Random.Range(0, 100);
            // Check again after spawning the RatSpawner
            if (hilf < ChestSpawnPercantage && !SpawnedPositions.Contains(position))
            {
                InstantiatePrefabsThatSpawnOnMap(position, WhatGetSpawned.Chest);
                SpawnedPositions.Add(position);
            }
            hilf = Random.Range(0, 1000);
            if (hilf < devilSpawnPercantage && !SpawnedPositions.Contains(position))
            {
                InstantiatePrefabsThatSpawnOnMap(position, WhatGetSpawned.Devil);
                SpawnedPositions.Add(position);
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
                Instantiate(chests, new Vector3(position.x + 0.5f, position.y + 0.5f, 0f), Quaternion.identity, parentGameobject.transform);
                break;
            case WhatGetSpawned.Devil:
                Instantiate(devils, new Vector3(position.x + 0.5f, position.y + 0.5f, 0f), Quaternion.identity, parentGameobject.transform);
                break;
            case WhatGetSpawned.Up:
                Instantiate(stairsUp, new Vector3(position.x + 0.5f, position.y + 0.5f, 0f), Quaternion.identity, parentGameobject.transform);
                break;
            case WhatGetSpawned.Down:
                Instantiate(stairsDown, new Vector3(position.x + 0.5f, position.y + 0.5f, 0f), Quaternion.identity, parentGameobject.transform);
                break;
            case WhatGetSpawned.Saeule:
                Instantiate(Saeule, new Vector3(position.x + 0.5f, position.y + 1f, 0f), Quaternion.identity, parentGameobject.transform);
                break;
        }
    }
}
