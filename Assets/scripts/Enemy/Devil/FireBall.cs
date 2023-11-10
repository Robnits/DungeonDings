using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    private GameObject player;
    private float speed = 3f;
    private Rigidbody2D FireBallRB;
    private bool fireBallActive = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FireBallRB = GetComponent<Rigidbody2D>();
        fireBallActive = true;
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
        Destroy(gameObject);
        //GetComponent<DevilFireBall>().FireBallActive = false;
        
    }


    IEnumerator DestroyAfterTime()
    {

        if (fireBallActive == true)
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
            fireBallActive = false;
        }
        yield return null;

    }
}

