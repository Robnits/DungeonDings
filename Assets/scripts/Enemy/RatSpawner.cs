using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class RatSpawner : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private FloatSO ScoreSO;

    private float life = 10f;
    private float value = 10f;
    private float damage = 3f;
    [SerializeField]  
    float SpawnrateRats = 1f;
    private bool playerIsInRange;

    public GameObject RatPrefab;
    private Rigidbody2D RatSpawnerRB;


    private void Start()
    {
        player = GameObject.Find("Player");
        RatSpawnerRB = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == gameObject.CompareTag("Player")) 
            playerIsInRange = true;
    }
    void Update()
    {
        if (life <= 0)
            Death();
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

    IEnumerator SpawnRatsTimer()
    {
       
        while (playerIsInRange)
        {
            yield return new WaitForSeconds(SpawnrateRats);
            SpawnRats();
        }
    }

    void SpawnRats()
    {
        int SpawnPosX = Random.Range(-2, 2);
        int SpawnPosY = Random.Range(-2, 2);

        Instantiate(RatPrefab, new Vector2(transform.position.x + SpawnPosX, transform.position.y + SpawnPosY), Quaternion.identity);
    }
}


