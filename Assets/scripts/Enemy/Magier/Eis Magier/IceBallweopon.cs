using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallweopon : MonoBehaviour
{
    // Start is called before the first frame updatepublic GameObject fireBallPrefab;
   
    public Transform firepoint;
    private readonly float fireforce = 2f;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(shootInterval());
    }

    IEnumerator shootInterval()
    {
        yield return new WaitForSeconds(5);
        FireFireball();
        StartCoroutine(shootInterval());
    }
    public void FireFireball()
    {
        // GameObject bullet = Instantiate(iceBallPrefab, firepoint.position, firepoint.rotation);
        // bullet.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * fireforce;
    }
}