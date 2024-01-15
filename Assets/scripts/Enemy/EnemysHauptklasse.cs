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
    public Healthbarscriptenemys healthscript;

    protected float speed;
    public float life;
    protected float value;
    protected float damage;
    protected float droprate;
    protected float maxlife;

    protected void Death()
    {
        if (transform.parent != null)
            Destroy(transform.parent.gameObject);
        else 
            Destroy(gameObject);
        GlobalVariables.money += value;
        if (Random.Range(0,100) < droprate)
            Instantiate(DropPotion, transform.position, Quaternion.identity);
    }
    private void Start()
    {
        maxlife = 20f;
    }

    private void Update()
    {
        if (healthscript != null)
            healthscript.FollowEnemy(gameObject.transform.position);
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

            if (healthscript != null)
                healthscript.GetDamaged(life); 

                
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Granade"))
        {
            life -= player.GetComponent<Player_behjaviour>().GetDamage(true);
            if (life <= 0)
                Death();
            if (healthscript != null)
                healthscript.GetDamaged(life);
        }
    }
}
