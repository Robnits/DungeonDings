using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firepoint;

    private int ammunition;

    private Player_Stats stats;

    private void Awake()
    {
        stats = gameObject.GetComponentInParent<Player_Stats>();
    }
    public float GetDamage()
    {
        return stats.baseDamage * stats.damage;
    }
        

    public void FireCounter()
    {
        

        if( ammunition < stats.maxAmmunition)
        {
            ammunition++;
            StartCoroutine(Shoot());
        } 
        
    }
    IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * stats.fireForce, ForceMode2D.Force);
        yield return new WaitForSeconds(stats.attackSpeed);
        ammunition--;
    }
}
