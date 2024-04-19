using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eisboden : MonoBehaviour
{
    public PlayerBehaviour player; 
    public GameObject damagePrefab; 

    private float lastSpawnTime;
    private readonly float spawnInterval = 8f; // Intervall für den Eisboden-Spawn

    private CircleCollider2D cc2D;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
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
        Vector3 spawnPosition = player.transform.position; 
        GameObject damageInstance = Instantiate(damagePrefab, spawnPosition, Quaternion.identity);
        Destroy(damageInstance, spawnInterval); 
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
            player.OnSlow(1.5f);

            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.NotOnSlow();
           
        }
    }
}
