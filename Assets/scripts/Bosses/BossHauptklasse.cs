using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossHauptklasse : MonoBehaviour
{
    protected GameObject player;
    public GameObject healthbar;
    protected float speed;
    public float life;
    protected float damage;
    public float maxlife;
    public Neeko neeko;
    public NeekoClones neekoClones;
    public GameObject sprechblase;
    public PhaseTrackScript phaseTrackScript;


    protected void Death()
    {
        if (transform.parent != null)
            Destroy(transform.parent.gameObject);
        else
            Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if(gameObject.GetComponent<NeekoClones>() == null)
                if(neeko.BattlePhase == 1 || neeko.BattlePhase == 2)
                    life -= player.GetComponent<PlayerBehaviour>().GetDamage(false);
            if (life <= 0 && neeko.phaseTrackScript.phasen == 1){
                StartCoroutine(phaseTrackScript.PhaseChange());
            }else if (life <= 0 && neeko.phaseTrackScript.phasen == 2)
            {
                life = 0;
                neeko.SetHealthbar();
                Destroy(transform.parent.gameObject);
            }
            if (neeko != null)
                neeko.SetHealthbar();
            if (neekoClones != null)
                StartCoroutine(neekoClones.GetComponent<NeekoClones>().Banter());           
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Granade"))
        {
            if(gameObject.GetComponent<NeekoClones>() == null)
                if(neeko.BattlePhase == 1 || neeko.BattlePhase == 2)
                    life -= player.GetComponent<PlayerBehaviour>().GetDamage(true);
            if (life <= 0 && neeko.phaseTrackScript.phasen == 1){
                StartCoroutine(phaseTrackScript.PhaseChange());
            }else if (life <= 0 && neeko.phaseTrackScript.phasen == 2)
            {
                life = 0;
                neeko.SetHealthbar();
                Destroy(transform.parent.gameObject);
            }
            if (neeko != null)
                neeko.SetHealthbar();
            if (neekoClones != null)
                StartCoroutine(neekoClones.GetComponent<NeekoClones>().Banter());
        }
    }
}
