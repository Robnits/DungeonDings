using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Rats : EnemysHauptklasse
{

    private Rigidbody2D rb;
    private void Start()
    {
        speed = 1.0f;
        life = 3f;
        value = 5f;
        damage = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (life <= 0)
            Death();

        transform.LookAt(player.transform.position);
        transform.Rotate(new Vector2(0, -90), Space.Self);//correcting the original rotation

        /* 
         
         ///
         /// habe beide Fehler mit dem durch die wand bewegen und nicht in den Spieler gehen gefixed.
         /// 
        if (Vector3.Distance(transform.position, player.transform.position) > 1f)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            rb.velocity
        }*/

        // Move towards the player
        if (Vector3.Distance(transform.position, player.transform.position) > 0f)
        {
            Vector2 moveDirection = (player.transform.position - transform.position).normalized;
            rb.velocity = moveDirection * speed;
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop moving if close to the player.
        }
    }

  

 


}
