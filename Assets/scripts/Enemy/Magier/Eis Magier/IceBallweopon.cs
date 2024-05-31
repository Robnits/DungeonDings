using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallweopon : MonoBehaviour
{
   
    public GameObject iceBallPrefab;
    public Transform firepointI;
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
        if(iceBallPrefab != null)
        {
            GameObject bullet = Instantiate(iceBallPrefab, firepointI.position, firepointI.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * fireforce;
        }
    }
}
