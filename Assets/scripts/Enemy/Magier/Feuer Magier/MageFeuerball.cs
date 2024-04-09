using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageFeuerball : MonoBehaviour
{

    private GameObject player;
    //private GameObject devil;
    private readonly float speed = 2f;
    private Rigidbody2D MageFeuerballRB;
    private bool MageFeuerballActive = true;
    private readonly float damage = 10f;
    private DealDamageToPlayer ddtp;

    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        MageFeuerballRB = GetComponent<Rigidbody2D>();
        MageFeuerballActive = true;
        ddtp = GetComponent<DealDamageToPlayer>();
        ddtp.dmg = damage;
    }

    private void Update()
    {
        if (player != null)
        {

            Vector3 directionToPlayer = player.transform.position - transform.position;

            // Calculate the angle to rotate towards the player
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            // Rotate the enemy to face the player
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (Vector3.Distance(transform.position, player.transform.position) > 0f)
            {
                Vector2 moveDirection = (player.transform.position - transform.position).normalized;
                MageFeuerballRB.velocity = moveDirection * speed;
            }

            StartCoroutine(DestroyAfterTime());
        }
    }
    
    
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
    }
}