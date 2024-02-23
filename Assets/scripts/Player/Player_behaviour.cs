using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Player_behjaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Weapon weapon;
    private GameObject scenenwechsel;
    private Cam_Follow cam;
    private Animator levelLoader;

    
    [SerializeField] private Animator anim;
    Player_Stats stats;

    Vector2 moveDirection;
    Vector2 mousePosition;
    private Vector2 lastCorserPos;

    private bool invincibleAfterDmg = false;
    private GameObject sprechblase;
    private TextMeshPro sprechblaseText;
    private Light2D light2d;
    private Light2D zerilight2d;
    private bool lastcursorInput;
    private Vector2 CurrentMouseInput;

    private void Awake()
    {
        sprechblase = GameObject.Find("Sprechblase");
        sprechblaseText = sprechblase.GetComponentInChildren<TextMeshPro>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<Cam_Follow>();
        levelLoader = GameObject.Find("LevelLoader").GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().GetComponentInChildren<Animator>();
        stats = gameObject.GetComponent<Player_Stats>();
        scenenwechsel = GameObject.FindGameObjectWithTag("Respawn");
        rb = GetComponent<Rigidbody2D>();
        light2d = sprechblase.GetComponent<Light2D>();

        mousePosition = new Vector2(0,0);

        light2d.intensity = 0f;
        zerilight2d = GetComponentInChildren<Light2D>();
        if (GlobalVariables.isInBossFight)
        {
            //transform.position = new Vector3 (0, -30.9f, 0);
            transform.position = new Vector3(0, -2f, 0);

            zerilight2d.intensity = 0f;
        }

        sprechblase.GetComponent<SpriteRenderer>().enabled = false;
        sprechblaseText.text = null;
    }

    public float GetDamage(bool isGranade)
    {
        if (isGranade)
            return (3f);
        else
            return (stats.damage * stats.baseDamage);
    }

    public void Movement(float horizontalInput, float verticalInput)
    {
        if (horizontalInput != 0 || verticalInput != 0)
            anim.SetBool("isMoving", true);
        else
            anim.SetBool("isMoving", false);

        if (!dashing)
            moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        LookAtPlayer();
    }

<<<<<<< Updated upstream

    public void LookAtPlayer()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
=======
    public void LookAtPlayerCoursor(Vector2 courserPos)
    {
        lastcursorInput = true;
    if (courserPos != null && courserPos != Vector2.zero)
    {
        mousePosition = courserPos + new Vector2(transform.position.x, transform.position.y);
        lastCorserPos = courserPos;
    }
    }
    public void LookAtPlayerMouse(Vector2 courserPos)
    {
        lastcursorInput = false;
        mousePosition = courserPos;
>>>>>>> Stashed changes
    }

    private void Death()
    {
        levelLoader.SetTrigger("Start");
        scenenwechsel.GetComponent<Scenemanager>().StartSwitch(1);
        Destroy(gameObject);
    }
    
    public void MousePosition(Vector2 mousePos)
    {
        CurrentMouseInput = mousePos;
    }
    private void FixedUpdate()
    {
        if(lastcursorInput)
            mousePosition = lastCorserPos + new Vector2(transform.position.x, transform.position.y);
        else
            mousePosition = CurrentMouseInput;
        rb.velocity = new Vector2(moveDirection.x * stats.moveSpeed, moveDirection.y * stats.moveSpeed);
        Vector2 aimdirection = mousePosition - rb.position;
        float aimangle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimangle;
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 && !invincibleAfterDmg)
            StartCoroutine(InvincibleTime(collision));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && !invincibleAfterDmg && collision.gameObject.CompareTag("Enemy"))
            StartCoroutine(InvincibleTriggerTime(collision));
    }

    IEnumerator InvincibleTriggerTime(Collider2D collision)
    {
        invincibleAfterDmg = true;
        StartCoroutine(cam.CameraShake(0.2f, 1f));


        var collisionComponent = collision.gameObject.GetComponent<MonoBehaviour>();

        switch (collisionComponent)
        {
            case RiesenFeuerballJunge fireBall:
                stats.GetDamage(fireBall.GetDamage());
                break;
        }

        if (stats.life <= 0)
            Death();
        yield return new WaitForSeconds(1);
        invincibleAfterDmg = false;
    }

    IEnumerator InvincibleTime(Collision2D collision)
    {
        invincibleAfterDmg = true;
        StartCoroutine(cam.CameraShake(0.2f, 1f));


        var collisionComponent = collision.gameObject.GetComponent<MonoBehaviour>();

        switch (collisionComponent)
        {
            case FireBall fireBall:
                stats.GetDamage(fireBall.GetDamage());
                break;

            case Rats rats:
                stats.GetDamage(rats.GetDamage());
                break;
            case Wuestengegner wuestengegner:
                stats.GetDamage(wuestengegner.GetDamage());
                break;
        }

        if (stats.life <= 0)
            Death();
        yield return new WaitForSeconds(0.5f);
        invincibleAfterDmg = false;
    }

    public void Shooting()
    {
        weapon.FireCounter();
    }

    private bool dashing;
    private bool dashcooldown;
    public void Dash()
    {
        if (!dashing && !dashcooldown)
            StartCoroutine(DashTime());
    }

    IEnumerator DashTime()
    {
        float timePassed = 0;
        dashing = true;
        dashcooldown = true;

        while (timePassed < 0.4)
        {
            rb.AddForce(stats.moveSpeed * 50 * moveDirection, ForceMode2D.Force);
            timePassed += Time.fixedDeltaTime;
            yield return null;
        }
        dashing = false;
        timePassed = 0;
        while (timePassed < stats.dashmaxCooldown * 50 + 1) // 50 muss zu hundert werden
        {
            stats.DashUI(timePassed);
            yield return new WaitForSeconds(0.01f);
            timePassed++;
        }

        dashcooldown = false;
    }

    public void SprechblasePressE(bool hilf)
    {
        if (hilf)
        {
            sprechblase.GetComponent<SpriteRenderer>().enabled = true;
            sprechblaseText.text = "Press E";
            light2d.intensity = 0.8f;
        }
        else
        {
            sprechblase.GetComponent<SpriteRenderer>().enabled = false;
            sprechblaseText.text = null;
            light2d.intensity = 0f;
        }
    }
    public void ThrowGranade()
    {
        weapon.Granade();
    }
    private bool isSlowed;

    public void NotOnSlow()
    {
        isSlowed = false;
        stats.moveSpeed = stats.maxMoveSpeed;
    }
    public void OnSlow(float slow)
    { 
        if (!isSlowed)
        {
            isSlowed = true;
            stats.moveSpeed -= slow;
        }
    }
}