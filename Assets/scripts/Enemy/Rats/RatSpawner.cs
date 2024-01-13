using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RatSpawner : EnemysHauptklasse
{
    [SerializeField]
    private Sprite open1, open2, closed;

    [SerializeField]  
    private float SpawnrateRats = 8f;

    private bool wasPlayerInRange = false;

    private SpriteRenderer render;

    public GameObject RatPrefab;

    private bool isActive = false;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

        SetRandomOpenSprite();

        life = 10f;
        value = 10f;
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!wasPlayerInRange)
                StartCoroutine(SpawnRatsTimer());
            wasPlayerInRange = true;            
        }
    }

    public bool ForChildIsOpen()
    {
        return isActive;
    }
    public void IsOpenOrClosed(bool IsOpen)
    {
        if (IsOpen)
        {
            StartCoroutine(SpawnRatsTimer());
            SetRandomOpenSprite();
        }
        else
        {
            render.sprite = closed;
            isActive = false;
        }
    }
    private void SetRandomOpenSprite()
    {
        if (Random.Range(0, 2) == 1)
            render.sprite = open1;
        else
            render.sprite = open2;
    }


    IEnumerator SpawnRatsTimer()
    {
        SpawnRats();
        isActive = true;
        yield return new WaitForSeconds(SpawnrateRats);

        if (isActive)
            StartCoroutine(SpawnRatsTimer());
        
    }

    void SpawnRats()
    {
        Instantiate(RatPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }
}