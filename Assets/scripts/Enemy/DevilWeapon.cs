using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilFireBall : MonoBehaviour
{
    
    public GameObject fireBallPrefab;
    public Transform firepoint;
    public bool FireBallActive = false;
    private float fireforce = 3f;
    public Transform player;
    public Transform target;
    public Transform start;

    private float baseDamage = 3f;


    public float GetDamage()
    {
        return baseDamage;
    }


    public void fireFireball()
    {

        GameObject bullet = Instantiate(fireBallPrefab, firepoint.position, firepoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * fireforce;
        FireBallActive = true;

        
    }
}
