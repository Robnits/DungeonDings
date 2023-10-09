using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Rats : EnemysHauptklasse
{
    private void Start()
    {
        speed = 3.0f;
        life = 3f;
        value = 5f;
        damage = 1f;
    }



    void Update()
    {
        //player = GameObject.Find("Player");

        if (life <= 0)
            Death();

        transform.LookAt(player.transform.position);
        transform.Rotate(new Vector2(0, -90), Space.Self);//correcting the original rotation

        //move towards the player
        if (Vector3.Distance(transform.position, player.transform.position) > 1f)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        }
    }
}
