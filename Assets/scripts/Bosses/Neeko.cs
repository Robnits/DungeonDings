using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Neeko : BossHauptklasse
{
    private int BattlePhase = 0;
    public float circleRadius = 5f;
    private GameObject rotationPoint;
    public PhaseTrackScript phaseTrackScript;
    private Rigidbody2D rb;
    public GameObject firePoint;
    public GameObject NeekoProjectile;
    private bool canshootagain = true;
    private bool isMovingRandomly;
    private Vector2 randomMoveDirection;
    private float randomMoveDuration = 1.0f;
    private float randomMoveTimer = 1f;

    public bool playerIsInRange;
    public float delay = 0.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (phaseTrackScript == null)
            phaseTrackScript = GameObject.Find("PhaseTracker").GetComponent<PhaseTrackScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        life = 100f;
        damage = 5;
        speed = 2.2f;
        SetHealthbar();
    }

    public void SetHealthbar()
    {
        if(healthbar != null)
            healthbar.transform.localScale = new Vector3(life / 100, 1, 1);
    }

    private void Update()
    {
        switch (BattlePhase)
        {
            case 0:
                    
                break;
            case 1:
                if(canshootagain)
                    transform.position = rotationPoint.transform.position;
                    LookAtPlayer();
                    StartCoroutine(Shoot());
                break;
            case 2:
                if (isMovingRandomly)
                {
                    randomMoveTimer += Time.deltaTime;
                    LookAtPlayer();
                    if (randomMoveTimer < randomMoveDuration)
                        rb.velocity = randomMoveDirection * speed;
                    else
                    {
                        isMovingRandomly = false;
                        rb.velocity = Vector2.zero;
                    }
                }
                break;
        }
    }
    private void FixedUpdate() {
        if (BattlePhase == 2)
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

    public void StartRotate(GameObject phaseRotationPoint)
    {
        rotationPoint = phaseRotationPoint;
        BattlePhase++;
    }

    private void LookAtPlayer()
    {
        Vector2 aimdirection = new Vector2(player.transform.position.x,player.transform.position.y) - rb.position;
        float aimangle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg + 90;
        rb.rotation = aimangle;
    }

    IEnumerator Shoot()
    {
        canshootagain = false;
        yield return new WaitForSeconds(Random.Range(2,5));
        GameObject bullet = Instantiate(NeekoProjectile, firePoint.transform.position, firePoint.transform.rotation * Quaternion.Euler(0, 0, 90));
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * 2, ForceMode2D.Force);
        StartCoroutine(Shoot());
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
                randomMoveDirection = Random.insideUnitCircle.normalized;
                randomMoveTimer = 0.0f;
            }
            else if (playerIsInRange)
            {
                isMovingRandomly = false;
            }

            yield return new WaitForSeconds(delay);

        }
    }

}
