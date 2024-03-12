using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageFeuerballweopon : MonoBehaviour
{
    public GameObject fireBallPrefab;
   
    public Transform firepoint;
    private readonly float fireforce = 2f;
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


}
