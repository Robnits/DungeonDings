using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    private GameObject player;
    //private GameObject devil;
    private readonly float speed = 2f;
    private Rigidbody2D FireBallRB;
    private bool fireBallActive = true;
    private readonly float damage = 30f;
    private DealDamageToPlayer ddtp;

    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        FireBallRB = GetComponent<Rigidbody2D>();
        fireBallActive = true;
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
                FireBallRB.velocity = moveDirection * speed;
            }

            StartCoroutine(DestroyAfterTime());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);        
    }

    IEnumerator DestroyAfterTime()
    { 
        if (fireBallActive)
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
            fireBallActive = false;
        }
    }
}

