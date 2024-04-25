using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageToPlayer : MonoBehaviour
{
    public bool poisondmg;
    public bool firedmg;
    public float dmg;
    public float dot;// damage over time
    public float damageTime;

    public float GetDamage()
    {
        return dmg;
    }
    public float GetTime()
    {
        if(poisondmg || firedmg)
            return damageTime;
        else 
            return 0;
    }
    public float DamageOverTime(){
        return dot;
    }
}
