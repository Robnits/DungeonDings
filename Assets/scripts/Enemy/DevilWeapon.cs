using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilFireBall : MonoBehaviour
{
    
    public GameObject fireBallPrefab;
    public Transform firepoint;
    public bool FireBallActive = false;
    private float fireforce = 50f;

    private float baseDamage = 3f;


    public float GetDamage()
    {
        return baseDamage;
    }


    public void fireFireball()
    {
        GameObject FireBall = Instantiate(fireBallPrefab,firepoint.position, firepoint.rotation);
        FireBallActive = true;
    }
}
