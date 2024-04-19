using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class MageDamage : MonoBehaviour
{
    public GameObject player;
    public GameObject damagePrefab;

    private float lastSpawnTime;
    private readonly float spawnInterval = 8f; // Ändere das Spawn-Intervall auf 8 Sekunden
    private readonly float damageDuration = 5f; // Lebensdauer des Schadens

    private CircleCollider2D cc2D;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lastSpawnTime = Time.time;
        cc2D = GetComponent<CircleCollider2D>();
        StartCoroutine(EnableColliderAfterDelay());
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > spawnInterval)
        {
            SpawnDamage();
            lastSpawnTime = Time.time;
        }
    }

    void SpawnDamage()
    {
        Vector3 spawnPosition = player.transform.position - new Vector3(0f, 0f, 0f);
        if (damagePrefab != null)
        {
            GameObject damageInstance = Instantiate(damagePrefab, spawnPosition, Quaternion.identity);
            Destroy(damageInstance, damageDuration); // Zerstöre das Schadensobjekt nach damageDuration Sekunden
        }
    }
   
    IEnumerator EnableColliderAfterDelay()
    {
        cc2D.enabled = false;
        yield return new WaitForSeconds(1.5f);
        cc2D.enabled = true;
    }
}
