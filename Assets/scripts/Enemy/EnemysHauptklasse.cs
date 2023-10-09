using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysHauptklasse : MonoBehaviour
{ 
    public GameObject player;


    [SerializeField]
    protected FloatSO ScoreSO;

    protected float speed;
    protected float life;
    protected float value;
    protected float damage;

    void Update()
    {
        //player = GameObject.Find("Player");

        if (life <= 0)
            Death();
    }

    protected void Death()
    {
        ScoreSO.NewMoney += value;
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            life -= player.GetComponent<Player_behjaviour>().GetDamage();
    }
}
