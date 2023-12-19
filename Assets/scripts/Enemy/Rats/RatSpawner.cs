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
    private float SpawnrateRats = 8f;
    private bool test = true;

    public GameObject RatPrefab;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (test)
                StartCoroutine(SpawnRatsTimer());
            test = false;
        }
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
            life -= player.GetComponent<Player_behjaviour>().GetDamage();
    }

    IEnumerator SpawnRatsTimer()
    {
        SpawnRats();
        yield return new WaitForSeconds(SpawnrateRats);
        

        StartCoroutine(SpawnRatsTimer());
    }

    void SpawnRats()
    {
        float SpawnPosX = Random.Range(-1f, 1f);
        float SpawnPosY = Random.Range(-1f, 1f);

        Instantiate(RatPrefab, new Vector3(transform.position.x + SpawnPosX, transform.position.y + SpawnPosY, 0), Quaternion.identity);
    }
}