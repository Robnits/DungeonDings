using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Neeko : BossHauptklasse
{
    public int BattlePhase = 0;
    public float circleRadius = 5f;
    public bool talkIsOver = false;
    private GameObject rotationPoint;
    private Rigidbody2D rb;
    public GameObject firePoint;
    public GameObject NeekoProjectile;
    public GameObject phase2NeekoProjectile;
    public GameObject plantPrefab;
    private bool canshootagain = true;
    private bool isMovingRandomly;
    private Vector2 randomMoveDirection;
    private float randomMoveDuration = 1.0f;
    private float randomMoveTimer = 1f;

    public bool playerIsInRange;
    public float delay = 0.0f;

    private int counter = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (phaseTrackScript == null)
            phaseTrackScript = GameObject.Find("PhaseTracker").GetComponent<PhaseTrackScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        maxlife = 10;
        life = maxlife;
        damage = 5;
        speed = 2.2f;
    }
    public void NextPhase(){
        BattlePhase ++;
    }
    public void SetHealthbar()
    {
        if(healthbar != null)
        {
            healthbar.transform.localScale = new Vector3(life / maxlife, 1, 1);
            if(life < 0)
                healthbar.transform.localScale = new Vector3(0, 1, 1);
        }
    }

    private void Update()
    {
        LookAtPlayer();
        switch (BattlePhase)
        {
            case 0:
                    
                break;
            case 1:
                healthbar.GetComponentInParent<CanvasGroup>().alpha = 0;
                transform.position = rotationPoint.transform.position;
                
                if(canshootagain)
                    StartCoroutine(Shoot());
                
                break;
            case 2:
            
                healthbar.GetComponentInParent<CanvasGroup>().alpha = 1;
                
                if (isMovingRandomly && talkIsOver == true)
                {
                    randomMoveTimer += Time.deltaTime;
                    if (randomMoveTimer < randomMoveDuration)
                        rb.velocity = randomMoveDirection * speed;
                    else
                    {
                        isMovingRandomly = false;
                        rb.velocity = Vector2.zero;
                    }
                }
                
                break;
            case 3:
                Destroy(transform.parent.gameObject);
                break;
        }
    }
    private void FixedUpdate() {
        
        if (BattlePhase == 2 && talkIsOver == true)
        {
            if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance == 1.8)
            {
                StartCoroutine(MoveBackwards());
            }
            else
            {
                if (distance > 3.8)
                {
                    Vector2 moveDirection = (player.transform.position - transform.position).normalized;
                    rb.velocity = moveDirection * speed;
                    StartCoroutine(MoveForwards());
                }
                else
                    StartCoroutine(RandMove());
            }
        }

        }
    }

    public void StartRotate(GameObject phaseRotationPoint, int phase)
    {
        rotationPoint = phaseRotationPoint;
        BattlePhase = phase;
    }

    private void LookAtPlayer()
    {
        Vector2 aimdirection = new Vector2(player.transform.position.x,player.transform.position.y) - rb.position;
        float aimangle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg + 90;
        rb.rotation = aimangle;
    }


    private bool waitFirst = true;
    IEnumerator Shoot()
    {            
        
        canshootagain = false;
        if (BattlePhase == 1){
            yield return new WaitForSeconds(UnityEngine.Random.Range(2,5));
            GameObject bullet = Instantiate(NeekoProjectile, firePoint.transform.position, firePoint.transform.rotation * Quaternion.Euler(0, 0, 90));
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * 2, ForceMode2D.Force);
        }else{
            if(waitFirst){
                yield return new WaitForSeconds(1f);
                waitFirst = false;
            }
            yield return new WaitForSeconds(0.5f);
            if(counter == 2){
                counter = 0;
                ShootPlant(firePoint.transform.rotation);
                yield return new WaitForSeconds(1);
            }
            for (int i = -3; i < 3; i++)
            {
                Quaternion rotation = firePoint.transform.rotation * Quaternion.Euler(0, 0, 180 + i * 3);
                GameObject bullet = Instantiate(phase2NeekoProjectile, firePoint.transform.position, rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * 10, ForceMode2D.Impulse);
                yield return new WaitForSeconds(0.1f);
            }
            counter ++;
        }
        StartCoroutine(Shoot());
    }

    private void ShootPlant(Quaternion angleToPLayer)
    {
        GameObject plant = Instantiate(plantPrefab, firePoint.transform.position, quaternion.Euler(0, 0, UnityEngine.Random.Range(-45, 45)) * angleToPLayer);
        plant.GetComponent<Rigidbody2D>().AddForce(plant.transform.up * 3, ForceMode2D.Impulse);
    }

    IEnumerator MoveBackwards()
    {
        float backwardDuration = 1f;
        float timePassed = 0;
        while (timePassed < backwardDuration && player != null)
        {
            Vector2 moveDirection = (transform.position - player.transform.position).normalized;
            rb.velocity = moveDirection * speed;
            timePassed += Time.deltaTime;

            yield return null;
        }
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(delay);
    }
    IEnumerator MoveForwards()
    {
            float forwardDuration = 1f;
            float timePassed = 0;
            while (timePassed < forwardDuration && player != null)
            {
                Vector2 moveDirection = (player.transform.position - transform.position).normalized;
                rb.velocity = moveDirection * speed;
                timePassed += Time.deltaTime;

                yield return null;
            }
            rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(delay);
    }

    IEnumerator RandMove()
    {   
        while (true)
        {
            if (!playerIsInRange && !isMovingRandomly)
            {
                isMovingRandomly = true;
                randomMoveDirection = UnityEngine.Random.insideUnitCircle.normalized;
                randomMoveTimer = 0.0f;
            }
            else if (playerIsInRange)
                isMovingRandomly = false;
            
            yield return new WaitForSeconds(delay);

        }
    }

    public IEnumerator Sprechblaseninhalt(string text, float time){
        sprechblase.GetComponent<SpriteRenderer>().enabled = true;
        sprechblase.GetComponentInChildren<TextMeshPro>().text = text;
        yield return new WaitForSeconds(time);
        sprechblase.GetComponent<SpriteRenderer>().enabled = false;
        sprechblase.GetComponentInChildren<TextMeshPro>().text = "";
    }

}
