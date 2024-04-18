using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageFireball : MonoBehaviour
{
    private GameObject player;
    private readonly float fireDamage = 5f;
    private DealDamageToPlayer ddtp;
    private float lastCastTime;

    public Rigidbody2D fireballPrefab;
    public float fireballSpeed = 8f;
    public float castInterval = 0.2f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ddtp = GetComponent<DealDamageToPlayer>();
        ddtp.dmg = fireDamage;
        lastCastTime = castInterval;
    }

    private void Update()
    {
        if (player == null || Time.time - lastCastTime <= castInterval)
            return;

        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        Vector2 fireballDirection = -directionToPlayer;
        Vector2 fireballSpawnPoint = (Vector2)transform.position + fireballDirection * 1f;

        Rigidbody2D fireballInstance = Instantiate(fireballPrefab, fireballSpawnPoint, Quaternion.identity);
        fireballInstance.velocity = fireballDirection * fireballSpeed;
        lastCastTime = Time.time;

        StartCoroutine(DestroyAfterTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            StartCoroutine(DestroyTime());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
            Destroy(gameObject);
    }

    private IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
