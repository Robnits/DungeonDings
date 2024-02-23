using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int piercing = 0; // Piercing-Counter

    private Rigidbody2D rb;
    private Vector2 geschwindigkeit;
    private void Start() {
        //Time.timeScale = 0.1f;
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        geschwindigkeit = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (piercing > 0 && collision.gameObject.layer == 6)
        {
            
            piercing--; // Verringere den Piercing-Z�hler
            rb.velocity = geschwindigkeit;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update() {
        geschwindigkeit = rb.velocity;
    }
}
