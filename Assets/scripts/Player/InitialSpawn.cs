using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSpawn : MonoBehaviour
{
    public GameObject player_prefab;

    private void Awake()
    {
        Instantiate(player_prefab, gameObject.transform);
    }
}
