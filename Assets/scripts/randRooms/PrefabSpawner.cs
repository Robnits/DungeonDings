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

    [SerializeField] 
    private GameObject chests;
    [SerializeField]
    [Range(0, 100)]
    private float ChestSpawnPercantage;

    [SerializeField] 
    private GameObject devils;
    [SerializeField]
    [Range(0, 100)]
    private float devilSpawnPercantage;

    [SerializeField]
    private GameObject wuestengegner;
    [SerializeField]
    [Range(0, 100)]
    private float wuestengegnerSpawnPercantage;
    [SerializeField] private GameObject mage;
    [SerializeField] private GameObject iceMage;
    private float mageSpawnrate;
    private float icemageSpawnrate;

    [SerializeField]
    private GameObject Saeule;

    private void Difficulty(BalancingSystemSO difficult)
    {
        SpawnerSpawnPercantage = difficult.spawnSpawner;
        ChestSpawnPercantage = difficult.spawnChest;
        devilSpawnPercantage = difficult.devilSpawnrate;
        wuestengegnerSpawnPercantage = difficult.wuestengegnerSpawnPercantage;
        mageSpawnrate = difficult.mageSpawnrate;
        icemageSpawnrate = difficult.icemageSpawnrate;
    }

    public enum WhatGetSpawned
    {
        RatSpawner,
        Chest,
        Exit,
        Devil,
        Down,
        Up,
        Saeule,
        wuestengegner,
        icemage,
        mage
    }

    private int biom;
    private enum WhichBiom
    {
        Default,
        Snow,
        Jungle
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
        SpawnObjects(floorPositions);
    }

    public void WhatshouldSpawn(int a)
    {
        biom = a;
    }

    private int GiveRandomNumber(){
        return Random.Range(0, 1000);
    }

    public void SpawnObjects(HashSet<Vector2Int> floorPositions)
    {
        
        foreach (Vector2Int position in floorPositions)
        {
            if(position.x >= 5 || position.x <= -5 && position.y >= 5 || position.y <= -5)
            {
                switch ((WhichBiom)biom)
                {
                    case WhichBiom.Default:
                        if (GiveRandomNumber() < SpawnerSpawnPercantage && !SpawnedPositions.Contains(position))
                            InstantiatePrefabsThatSpawnOnMap(position, WhatGetSpawned.RatSpawner);
                        if (GiveRandomNumber() < devilSpawnPercantage && !SpawnedPositions.Contains(position))
                            InstantiatePrefabsThatSpawnOnMap(position, WhatGetSpawned.Devil);
                        if (GiveRandomNumber() < mageSpawnrate && !SpawnedPositions.Contains(position))
                            InstantiatePrefabsThatSpawnOnMap(position, WhatGetSpawned.mage);
                        break;
                    case WhichBiom.Jungle:
                        if (GiveRandomNumber() < wuestengegnerSpawnPercantage && !SpawnedPositions.Contains(position))
                            InstantiatePrefabsThatSpawnOnMap(position, WhatGetSpawned.wuestengegner);
                        break;
                    case WhichBiom.Snow:
                        if (GiveRandomNumber() < icemageSpawnrate && !SpawnedPositions.Contains(position))
                            InstantiatePrefabsThatSpawnOnMap(position, WhatGetSpawned.icemage);
                        break;
                }              
                if (GiveRandomNumber() < ChestSpawnPercantage && !SpawnedPositions.Contains(position))
                    InstantiatePrefabsThatSpawnOnMap(position, WhatGetSpawned.Chest);
            }
        }
    }

    private void InstantiatePrefabsThatSpawnOnMap(Vector2Int position, WhatGetSpawned whatGetGenerated)
    {
        
        SpawnedPositions.Add(position);
        
        switch (whatGetGenerated)
        {
            case WhatGetSpawned.RatSpawner:
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
            case WhatGetSpawned.wuestengegner:
                Instantiate(wuestengegner, new Vector3(position.x + 0.5f, position.y + 1f, 0f), Quaternion.identity, parentGameobject.transform);
                break;
            case WhatGetSpawned.mage:
                Instantiate(mage, new Vector3(position.x + 0.5f, position.y + 1f, 0f), Quaternion.identity, parentGameobject.transform);
                break;
            case WhatGetSpawned.icemage:
                Instantiate(iceMage, new Vector3(position.x + 0.5f, position.y + 1f, 0f), Quaternion.identity, parentGameobject.transform);
                break;
        }
    }
}