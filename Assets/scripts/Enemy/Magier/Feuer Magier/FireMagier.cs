using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Firemagier : EnemysHauptklasse
{

    private Rigidbody2D rb;

    public bool playerIsInRange;

    void Start()
    {
        life = 5f;
        value = 10f;
        speed = 2.5f;
        droprate = 90;
        healthscript.GetMaxhealth(life);
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    

    void FixedUpdate()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);

            Vector3 directionToPlayer = player.transform.position - transform.position;

            // Calculate the angle to rotate towards the player
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg + 90;

            // Rotate the enemy to face the player
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (distance < 2.1)
            {
                StartCoroutine(MoveBackwards());
            }
            else
            {
                Vector2 moveDirection = (player.transform.position - transform.position).normalized;
                rb.velocity = moveDirection * speed;
            }

            
        }
    }

    IEnumerator MoveBackwards()
    {
        float timePassed = 0;
        while (timePassed < 2 && player != null)
        {
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;
            rb.velocity = speed * -1 * moveDirection;
            timePassed += Time.deltaTime;

            yield return null;
        }

    }
}

