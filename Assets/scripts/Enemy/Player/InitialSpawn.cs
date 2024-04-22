using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject player_prefab;

    private void Awake()
    {
        Instantiate(player_prefab);
    }
}
