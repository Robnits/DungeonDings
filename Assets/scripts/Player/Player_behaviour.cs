using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Weapon weapon;
    private GameObject sceneChange;
    private Cam_Follow cam;
    private Animator levelLoader;

    [SerializeField] private Animator anim;
    private Player_Stats stats;

    private Vector2 moveDirection;
    private Vector2 mousePosition;
    private Vector2 lastCursorPos;

    private bool invincibleAfterDmg = false;
    private GameObject speechBubble;
    private TextMeshPro speechBubbleText;
    private Light2D light2d;
    private Light2D zeroLight2d;
    private bool lastCursorInput;
    private Vector2 currentMouseInput;
    private bool dashing;
    private bool isDashAvailable;

    private void Awake()
    {
        InitializeComponents();

        mousePosition = Vector2.zero;
        light2d.intensity = 0f;

        if (GlobalVariables.isInBossFight)
        {
            // Adjust player position for boss fight
            transform.position = new Vector3(0, -2f, 0);
            zeroLight2d.intensity = 0f;
        }

        speechBubble.GetComponent<SpriteRenderer>().enabled = false;
        speechBubbleText.text = null;
    }

    private void InitializeComponents()
    {
        speechBubble = GameObject.Find("Sprechblase");
        speechBubbleText = speechBubble.GetComponentInChildren<TextMeshPro>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<Cam_Follow>();
        levelLoader = GameObject.Find("LevelLoader").GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().GetComponentInChildren<Animator>();
        stats = gameObject.GetComponent<Player_Stats>();
        sceneChange = GameObject.FindGameObjectWithTag("Respawn");
        rb = GetComponent<Rigidbody2D>();
        light2d = speechBubble.GetComponent<Light2D>();
        zeroLight2d = GetComponentInChildren<Light2D>();
    }

    public float GetDamage(bool isGranade)
    {
        return isGranade ? 3f : stats.damage * stats.baseDamage;
    }

    public void Movement(Vector2 movement)
    {
        anim.SetBool("isMoving", horizontalInput != 0 || verticalInput != 0);

        if (!dashing)
            moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
    }

    public void Movement(float horizontalInput, float verticalInput)
    {
        anim.SetBool("isMoving", movement.x != 0 || movement.y != 0);

        if (!dashing)
            moveDirection = new Vector2(movement.x, movement.y).normalized;
    }


    public void LookAtPlayer(Vector2 mousePos)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void Death()
    {
        levelLoader.SetTrigger("Start");
        sceneChange.GetComponent<Scenemanager>().StartSwitch(1);
        Destroy(gameObject);
    }

    public void MousePosition(Vector2 mousePos)
    {
        currentMouseInput = mousePos;
    }

    private void FixedUpdate()
    {
        mousePosition = lastCursorInput ? lastCursorPos + new Vector2(transform.position.x, transform.position.y) : currentMouseInput;
        rb.velocity = moveDirection * stats.moveSpeed;
        Vector2 aimdirection = mousePosition - rb.position;
        float aimangle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimangle;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<DealDamageToPlayer>() != null && !invincibleAfterDmg)
            StartCoroutine(InvincibleTime(collision));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DealDamageToPlayer>() != null && !invincibleAfterDmg)
            StartCoroutine(InvincibleTriggerTime(collision));
    }

    IEnumerator InvincibleTriggerTime(Collider2D collision)
    {
        invincibleAfterDmg = true;
        StartCoroutine(cam.CameraShake(0.2f, 1f));

        var dmgscript = collision.gameObject.GetComponent<DealDamageToPlayer>();
        StartCoroutine(stats.GetDamage(dmgscript.GetDamage(), dmgscript.GetTime(), dmgscript.DamageOverTime()));

        if (stats.life <= 0)
            Death();

        yield return new WaitForSeconds(1);
        invincibleAfterDmg = false;
    }

    IEnumerator InvincibleTime(Collision2D collision)
    {
        invincibleAfterDmg = true;
        StartCoroutine(cam.CameraShake(0.2f, 1f));

        var dmgscript = collision.gameObject.GetComponent<DealDamageToPlayer>();
        StartCoroutine(stats.GetDamage(dmgscript.GetDamage(), dmgscript.GetTime(), dmgscript.DamageOverTime()));

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
        if (!dashing && !isDashAvailable)
            StartCoroutine(DashTime());
    }

    IEnumerator DashTime()
    {
        float timePassed = 0;
        dashing = true;
        isDashAvailable = true;

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

        isDashAvailable = false;
    }

    public void SprechblasePressE(bool hilf)
    {
        if (hilf)
        {
            speechBubble.GetComponent<SpriteRenderer>().enabled = true;
            speechBubbleText.text = "Press E";
            light2d.intensity = 0.8f;
        }
        else
        {
            speechBubble.GetComponent<SpriteRenderer>().enabled = false;
            speechBubbleText.text = null;
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
