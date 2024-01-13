using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemysHauptklasse : MonoBehaviour
{ 
    protected GameObject player;
    public GameObject DropPotion;

    protected float speed;
    protected float life;
    protected float value;
    protected float damage;
    protected float droprate;

    protected void Death()
    {
        Destroy(gameObject);
        GlobalVariables.money += value;
        if (Random.Range(0,100) < droprate)
            Instantiate(DropPotion, transform.position, Quaternion.identity);
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
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Granade"))
        {
            life -= player.GetComponent<Player_behjaviour>().GetDamage(true);
            if (life <= 0)
                Death();
        }
    }
}
