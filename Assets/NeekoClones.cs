using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NeekoClones : BossHauptklasse
{
    private int BattlePhase = 0;
    public float circleRadius = 5f;
    private GameObject rotationPoint;
    public PhaseTrackScript phaseTrackScript;
    private Rigidbody2D rb;
    public GameObject firePoint;
    public GameObject NeekoProjectile;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (phaseTrackScript == null)
            phaseTrackScript = GameObject.Find("PhaseTracker").GetComponent<PhaseTrackScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        life = 10f;
        damage = 0;
        Sprechblase.transform.localScale = new Vector3(0.15f, 0.15f, 1);
        SetHealthbar();
    }

    public void SetHealthbar()
    {
        if(healthbar != null)
            healthbar.transform.localScale = new Vector3(life / 100, 1, 1);
    }
    private bool canshootagain;
    private void Update()
    { 
        Sprechblase.transform.localScale = new Vector3(0.15f, 0.15f, 1);
        switch (BattlePhase)
        {
            case 0:
                    
                break;
            case 1:
                transform.position = rotationPoint.transform.position;
                Vector2 aimdirection = new Vector2(player.transform.position.x,player.transform.position.y) - rb.position;
                float aimangle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg + 90;
                rb.rotation = aimangle;
                if(!canshootagain)
                    StartCoroutine(ShootslightlyRandom());
                break;
        }
    }
    IEnumerator ShootslightlyRandom()
    {
        canshootagain = true;
        GameObject bullet = Instantiate(NeekoProjectile, firePoint.transform.position, firePoint.transform.rotation * Quaternion.Euler(0, 0, 90));
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up, ForceMode2D.Force);
        yield return new WaitForSeconds(Random.Range(3, 10));
        canshootagain = false;
        StartCoroutine(ShootslightlyRandom());
    }


    public void StartRotate(GameObject phaseRotationPoint)
    {
        
        rotationPoint = phaseRotationPoint;
        BattlePhase++;
    }
}
