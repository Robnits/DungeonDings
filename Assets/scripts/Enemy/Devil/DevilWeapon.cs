using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilFireBall : MonoBehaviour
{
    
    public GameObject fireBallPrefab;
    public GameObject riesenfireBallPrefab;
    public Transform firepoint;
    private readonly float fireforce = 3f;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void FireFireball()
    {
        GameObject bullet = Instantiate(fireBallPrefab, firepoint.position, firepoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * fireforce;
    }

    public void RiesenFeuerballJunge()
    {
        GameObject bullet = Instantiate(riesenfireBallPrefab, firepoint.position, firepoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * fireforce;
    }

}
