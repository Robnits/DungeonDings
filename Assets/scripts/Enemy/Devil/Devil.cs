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
    private bool playerIsInMelee;

    private float Countdown = 3f;
    // Start is called before the first frame update
    void Start()
    {
        life = 10f;
        damage = 3f;
        value = 10f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (life <= 0)
            Death();

        if (playerIsInRange)// Guck den Spieler an
        {
            transform.LookAt(player.transform.position);
            transform.Rotate(new Vector2(0, -90), Space.Self);
            playerIsInRange = true;
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
