using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int piercing = 1; // Piercing-Counter

    private Rigidbody2D rb;
    private Vector2 geschwindigkeit;
    private float rotation;
    private bool hasEnteredTrigger = false; // Flag, um zu verhindern, dass Trigger mehrmals betreten wird

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && !hasEnteredTrigger)
        {
            rb = GetComponent<Rigidbody2D>();
            geschwindigkeit = rb.velocity;
            rotation = rb.rotation;
            hasEnteredTrigger = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (piercing > 0 && collision.gameObject.layer == 6)
        {
            piercing--; // Verringere den Piercing-Zähler
        }
        else
        {
            
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        // Deine Logik hier
        if (hasEnteredTrigger)
        {
            rb.velocity = geschwindigkeit;
            rb.rotation = rotation;
        }
    }
}
