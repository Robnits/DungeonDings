using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Rats : MonoBehaviour
{

    public GameObject player;


    [SerializeField]
    private FloatSO ScoreSO;

    private float speed = 3f;
    private float life = 3f;
    private float value = 10f;
    private float damage = 3f;
    public Transform target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
            Death();

        transform.LookAt(target.position);
        transform.Rotate(new Vector2(0, -90), Space.Self);//correcting the original rotation


        //move towards the player
        if (Vector3.Distance(transform.position, target.position) > 1f)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));

        }
    }

    void Death()
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
            life -= player.GetComponent<Player_behjaviour>().GetDamage();
    }
}
