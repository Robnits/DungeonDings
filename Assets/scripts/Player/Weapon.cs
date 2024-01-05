using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firepoint;
    private Player_Stats stats;

    [SerializeField]
    private AudioSource shoot;

    private int ammunition = 2;

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
