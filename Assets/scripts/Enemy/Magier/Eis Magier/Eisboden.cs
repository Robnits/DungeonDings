using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eisboden : MonoBehaviour
{
    public GameObject player; // Referenz auf den Spieler
    public GameObject damagePrefab; // Prefab für den Eisboden
    public float slowFactor = 0.5f; // Verlangsamungsfaktor für den Spieler

    private float lastSpawnTime;
    private readonly float spawnInterval = 8f; // Intervall für den Eisboden-Spawn

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
        Vector3 spawnPosition = player.transform.position; // Eisboden auf Spielerposition spawnen
        GameObject damageInstance = Instantiate(damagePrefab, spawnPosition, Quaternion.identity);
        Destroy(damageInstance, spawnInterval); // Eisboden nach spawnInterval Sekunden zerstören
    }

    IEnumerator EnableColliderAfterDelay()
    {
        cc2D.enabled = false;
        yield return new WaitForSeconds(1.5f);
        cc2D.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Den Spieler verlangsamen
            Player_Stats playerStats = other.gameObject.GetComponent<Player_Stats>();
            if (playerStats != null)
            {
                playerStats.moveSpeed *= slowFactor; // Verlangsame die Bewegungsgeschwindigkeit des Spielers
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Die Verlangsamung rückgängig machen, wenn der Spieler den Eisboden verlässt
            Player_Stats playerStats = other.gameObject.GetComponent<Player_Stats>();
            if (playerStats != null)
            {
                playerStats.moveSpeed /= slowFactor; // Stelle die normale Bewegungsgeschwindigkeit des Spielers wieder her
            }
        }
    }
}
