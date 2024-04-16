using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using System;
using JetBrains.Annotations;

public class Firemagier : EnemysHauptklasse
{

    private Rigidbody2D rb;
    private bool isrotating;

    public bool playerIsInRange;
    public float delay = 1f;

    void Start()
    {
        life = 10f;
        value = 10f;
        speed = 2f;
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

            if (distance < 1.5)
            {
                StartCoroutine(MoveBackwards());
            }
            else 
            {
                if (distance > 3)
                {
                    Vector2 moveDirection = (player.transform.position - transform.position).normalized;
                    rb.velocity = moveDirection * speed;
                } else 
                {
                    if (!isrotating)
                        StartCoroutine(Randmove());
                }
            }
        }
    }

    IEnumerator MoveBackwards()
    {
        float backwardDuration = 0.01f;
        float timePassed = 0;
        while (timePassed < backwardDuration && player != null)
        {
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;
            rb.velocity = speed * -1 * moveDirection;
            timePassed += Time.deltaTime;

            yield return null;
        }
        rb.velocity = Vector2.zero;
    }
    IEnumerator MoveSidewards1()
    {
        float timePassed = 0;
        while (timePassed < 1 && player != null)
        {
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;
            Vector2 perpendicularDirection = new Vector2(-moveDirection.y, moveDirection.x).normalized;
            rb.velocity = speed * perpendicularDirection;
            timePassed += Time.deltaTime;

            yield return null;
        }
    }
    IEnumerator MoveSidewards2()
    {
        float timePassed = 0;
        while (timePassed < 1 && player != null)
        {
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;
            Vector2 perpendicularDirection = new Vector2(moveDirection.y, -moveDirection.x).normalized;
            rb.velocity = speed * perpendicularDirection;
            timePassed += Time.deltaTime;

            yield return null;
        }
    }
    IEnumerator Randmove()
    {
        isrotating = true;
        int randommove = UnityEngine.Random.Range(0, 2);
        if (randommove == 0)
        {
            StartCoroutine(MoveSidewards1());
        }
        else
        {
            StartCoroutine(MoveSidewards2());
        }

        yield return new WaitForSeconds(delay);
        isrotating = false;
    }
}

