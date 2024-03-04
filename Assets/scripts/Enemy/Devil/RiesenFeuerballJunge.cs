using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiesenFeuerballJunge : MonoBehaviour
{

    private GameObject player;
    //private GameObject devil;
    private readonly float speed = 0.5f;
    private Rigidbody2D FireBallRB;
    private bool fireBallActive = true;
    private readonly float damage = 75f;

    private DealDamageToPlayer ddtp;

    private void Start()
    {
        ddtp = GetComponent<DealDamageToPlayer>();
        player = GameObject.FindGameObjectWithTag("Player");
        FireBallRB = GetComponent<Rigidbody2D>();
        fireBallActive = true;
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

    IEnumerator DestroyAfterTime()
    {
        if (fireBallActive)
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
            fireBallActive = false;
        }
    }
}
