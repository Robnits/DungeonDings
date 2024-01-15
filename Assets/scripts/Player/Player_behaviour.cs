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

    [SerializeField]
    Weapon weapon;

    private GameObject scenenwechsel;

    private Cam_Follow cam;
    private Animator levelLoader;

    [SerializeField]
    private Animator anim;
    Player_Stats stats;
    
    Vector2 moveDirection;
    Vector2 mousePosition;

    private bool invincibleAfterDmg = false;


    private GameObject sprechblase;
    private TextMeshPro sprechblaseText;
    private Light2D light2d;

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
        gameObject.transform.position = new Vector3(0, 0, 0);

        light2d.intensity = 0f;

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

        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Death()
    {
        levelLoader.SetTrigger("Start");
        scenenwechsel.GetComponent<Scenemanager>().StartSwitch(1);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * stats.moveSpeed, moveDirection.y * stats.moveSpeed);
        Vector2 aimdirection = mousePosition - rb.position;
        float aimangle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimangle;

        /*sprechblase.transform.rotation = quaternion.identity;
        Vector2 offset = new Vector2(0.6f, 0.45f); // Adjust the offset as needed
        sprechblase.transform.position = rb.position + offset;*/
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

    public void Dash()
    {
        rb.velocity += stats.moveSpeed * 3 * moveDirection;
    }

    public void CloseGulli(bool showText)
    {
        if (showText)
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
}
 