using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] 
    private GameObject bulletPrefab;
    [SerializeField] 
    private Transform firepoint;
    private player_Stats stats;
    [SerializeField] 
    private GameObject granadePrefab;

    [SerializeField]
    private AudioSource shoot;

    private int ammunition = 2;
    private bool hasGranade = true;
    private int currentmaxammo;

    public void ChangeBullets(int changedBullets)
    {
        ammunition += changedBullets;
    }

    private void Start()
    {
        
        stats = gameObject.GetComponentInParent<player_Stats>();
        ammunition = stats.maxAmmunition;
        currentmaxammo = stats.maxAmmunition;
    }

    private void Update()
    {
        if(currentmaxammo  < stats.maxAmmunition)
        {
            ammunition++;
            currentmaxammo = stats.maxAmmunition;   
        }
    }
    public void Granade()
    {
        if (hasGranade)
        {
            hasGranade = false;
            GameObject granade = Instantiate(granadePrefab, firepoint.position, firepoint.rotation * Quaternion.Euler(0, 0, 90));
            granade.GetComponent<Rigidbody2D>().AddForce(12 * stats.fireForce * firepoint.up, ForceMode2D.Force);
            StartCoroutine(GranadeCooldown());
        }
    }

    IEnumerator GranadeCooldown()
    {
        yield return new WaitForSeconds(10f);
        hasGranade = true;
    }

    public void FireCounter()
    {
        if( ammunition > 0)
        {
            ammunition--;
            stats.BulletChanges(ammunition);
            shoot.Play();
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation * Quaternion.Euler(0, 0, 90));
        bullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * stats.fireForce, ForceMode2D.Force);
        yield return new WaitForSeconds(stats.attackSpeed);
        ammunition++;
        stats.BulletChanges(ammunition);
    }
}
