using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageFeuerball : MonoBehaviour
{
    private GameObject player;
<<<<<<< Updated upstream
    //private GameObject devil;
    private readonly float speed = 2f;
    private Rigidbody2D MageFeuerballRB;
    private bool MageFeuerballActive = true;
    private readonly float damage = 10f;
=======
    private readonly float firedamage = 30f;
>>>>>>> Stashed changes
    private DealDamageToPlayer ddtp;

    public Rigidbody2D feuerballPrefab; 
    public float feuerballSpeed = 8f; 
    public float castInterval = 0.2f; 
    private float lastCastTime; 


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ddtp = GetComponent<DealDamageToPlayer>();
        ddtp.dmg = firedamage;
        lastCastTime = castInterval; 
    }

    void Update()
    {
        if (player != null && Time.time - lastCastTime > castInterval)
        {
            // Berechnen Sie die Richtung zum Spieler
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;

            // Umkehren der Richtung des Spielers, um die Flugrichtung des Projektils zu bestimmen
            Vector2 fireballDirection = -directionToPlayer;

            // Bestimmen Sie den Punkt, an dem das Projektil abgefeuert werden soll (zum Beispiel 1 Einheit vor dem Feuermagier)
            Vector2 fireballSpawnPoint = (Vector2)transform.position + fireballDirection * 1f;

            // Erstellen Sie das Projektil
            Rigidbody2D feuerballInstance = Instantiate(feuerballPrefab, fireballSpawnPoint, Quaternion.identity);

            // Setzen Sie die Geschwindigkeit des Projektils
            feuerballInstance.velocity = fireballDirection * feuerballSpeed;

            // Aktualisieren Sie die Zeit des letzten Feuerball-Casts
            lastCastTime = Time.time;

            StartCoroutine(DestroyAfterTime(feuerballInstance.gameObject)); // Übergeben Sie das GameObject des Projektils
        }
    }
<<<<<<< Updated upstream
    
    
    private void OnTriggerEnter2D(Collider2D colision){
        if (colision.gameObject.CompareTag("Player"))
        StartCoroutine(destroy());    
    }
    IEnumerator destroy(){
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
   IEnumerator DestroyAfterTime()
    { 
        if (MageFeuerballActive)
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
            MageFeuerballActive = false;
        }
=======

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    IEnumerator DestroyAfterTime(GameObject projectile)
    {
        yield return new WaitForSeconds(3f);
        Destroy(projectile); // Zerstöre das übergebene Projektil-GameObject
>>>>>>> Stashed changes
    }
}