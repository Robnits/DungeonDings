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
        sprechblaseText = GameObject.Find("Sprechblase").GetComponentInChildren<TextMeshPro>();
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

    public float GetDamage()
    {
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
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 && !invincibleAfterDmg)
        {
            if (collision.gameObject.GetComponentInChildren<Rats>() == true)
            {
                if (!invincibleAfterDmg)
                    StartCoroutine(InvincibleTime(collision));
            }
        }
    }

    IEnumerator InvincibleTime(Collision2D collision)
    {
        invincibleAfterDmg = true;
        StartCoroutine(cam.CameraShake(0.2f, 1f));
        stats.GetDamage(collision.gameObject.GetComponent<Rats>().GetDamage());
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
        rb.velocity += moveDirection * stats.moveSpeed;
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
}
 