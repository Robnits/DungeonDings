using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firepoint;
    private float fireforce = 10f;

    private float baseDamage = 3f;

    public float GetDamage()
    {
        return baseDamage;
    }


    public void fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position,firepoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * fireforce, ForceMode2D.Force);
    }
}
