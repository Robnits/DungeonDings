using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firepoint;
    private int ammunition;
    private int maxAmmunition = 2;
    private float fireforce = 30f;


    private float baseDamage = 3f;

    public float GetDamage()
    {
        return baseDamage;
    }
        

    public void FireCounter()
    {
        

        if( ammunition < maxAmmunition)
        {
            ammunition++;
            StartCoroutine(Shoot());
        }
        
    }
    IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * fireforce, ForceMode2D.Force);
        print(ammunition);
        yield return new WaitForSeconds(1f);
        ammunition--;
    }
}
