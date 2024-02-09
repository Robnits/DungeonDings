using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossHauptklasse : MonoBehaviour
{
    protected GameObject player;
    public GameObject healthbar;
    protected float speed;
    public float life;
    protected float damage;
    protected float maxlife;
    public Neeko neeko;
    public NeekoClones neekoClones;
    public GameObject Sprechblase;

    protected void Death()
    {
        if (transform.parent != null)
            Destroy(transform.parent.gameObject);
        else
            Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            life -= player.GetComponent<Player_behjaviour>().GetDamage(false);
            if (life <= 0)
                Death();
            if(neeko != null)
                neeko.SetHealthbar();
            if (neekoClones != null)
                neekoClones.SetHealthbar();
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Granade"))
        {
            life -= player.GetComponent<Player_behjaviour>().GetDamage(true);
            if (life <= 0)
                Death();
            if (neeko != null)
                neeko.SetHealthbar();
            if(neekoClones != null)
                neekoClones.SetHealthbar();

        }
    }
}
