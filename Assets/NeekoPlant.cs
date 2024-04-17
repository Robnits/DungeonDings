using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NeekoPlant : EnemysHauptklasse
{
    public Transform[] firepoints = new Transform[8]; 
    

    [SerializeField]
    private GameObject featherProjectile;

    void Start()
    {
        InstantiateStats();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ThrowLeaf());
    }

    private void InstantiateStats()
    {
        life = 3f;
        value = 10f;
        droprate = 20;
    }

    IEnumerator ThrowLeaf()
    {
        yield return new WaitForSeconds(5);
        for (int i = 0; i < firepoints.Length; i++)
        {
            
            GameObject test =  Instantiate(featherProjectile, firepoints[i].position, firepoints[i].rotation);
            test.GetComponent<Rigidbody2D>().velocity = new ((firepoints[i].position.x - transform.position.x) * 10 , (firepoints[i].position.y - transform.position.y) * 10);
            print(firepoints[i]);
        }
        StartCoroutine(ThrowLeaf());
    }
}