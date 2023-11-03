using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Devil : EnemysHauptklasse
{
   
    public GameObject weapon;

    private bool playerIsInRange;
    public bool playerIsInMelee;
    private Quaternion targetRotation;

    private float Countdown = 3f;
    // Start is called before the first frame update
    void Start()
    {
        life = 10f;
        damage = 3f;
        value = 10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (life <= 0)
            Death();

        if (playerIsInRange)
        {
            Vector3 playerDirection = player.transform.position - transform.position;
            float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            // Smoothly rotate towards the player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10);
        }
    }

    private void Death()
    {
        ScoreSO.NewMoney += value;
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            life -= player.GetComponent<Player_behjaviour>().GetDamage();
            Debug.Log(life);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerIsInMelee == false)
        {
            playerIsInRange = true;
            StartCoroutine(FireballCountdown());
                
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsInRange = false;
        }
    }

  IEnumerator FireballCountdown()
    {
        while (playerIsInRange)
        {
            yield return new WaitForSeconds(Countdown);
            weapon.GetComponent<DevilFireBall>().fireFireball();
        }
        yield return null;
    }



}
