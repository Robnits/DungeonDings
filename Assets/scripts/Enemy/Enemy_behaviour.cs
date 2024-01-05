using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_behaviour : MonoBehaviour
{

    public GameObject player;
    public GameObject weapon;

    [SerializeField]
    private FloatSO ScoreSO;

    private float life = 10f;
    private float damage = 3f;
    //private float movementspeed = 0.1f;
    private float value = 10f;
    private bool playerIsInRange;
    private Quaternion targetRotation;


    void Update()
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
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsInRange = true;
            StartCoroutine(AutomaticShooting());
            
        }else if (collision.gameObject.CompareTag("Bullet"))
        {
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + , gameObject.transform.position.z);
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsInRange = false;
        }
    }

    IEnumerator AutomaticShooting()
    {
        while (playerIsInRange)
        {
            yield return new WaitForSeconds(1f);
            weapon.GetComponent<Enemy_Weapon>().fire();
            
        }
        yield return null;
        
    }
}
