using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysHauptklasse : MonoBehaviour
{ 
    protected GameObject player;

    protected float speed;
    protected float life;
    protected float value;
    protected float damage;

    protected void Death()
    {
        Destroy(gameObject);
        GlobalVariables.money += value;
    }

    public float GetDamage()
    {
        return damage;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            life -= player.GetComponent<Player_behjaviour>().GetDamage();
            if (life <= 0)
                Death();
        }
            
    }
}
