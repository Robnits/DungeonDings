using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    private GameObject player;
    private float speed = 3f;
    private Rigidbody2D FireBallRB;
    private bool fireBallActive = true;
    private float damage = 5f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FireBallRB = GetComponent<Rigidbody2D>();
        fireBallActive = true;
    }
    public float GetDamage()
    {
        return damage;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 0f)
        {
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;
            FireBallRB.velocity = moveDirection * speed;
        }

        StartCoroutine(DestroyAfterTime());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
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

