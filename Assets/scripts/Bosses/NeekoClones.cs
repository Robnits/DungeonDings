using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NeekoClones : BossHauptklasse
{
    private int BattlePhase = 0;
    public float circleRadius = 5f;
    private GameObject rotationPoint;
    private Rigidbody2D rb;
    public GameObject firePoint;
    public GameObject NeekoProjectile;
    private bool canshootagain = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (phaseTrackScript == null)
            phaseTrackScript = GameObject.Find("PhaseTracker").GetComponent<PhaseTrackScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        life = 10f;
        damage = 0;
        Sprechblase.transform.localScale = new Vector3(0.15f, 0.15f, 1);
    }

    private void Update()
    { 
        Sprechblase.transform.localScale = new Vector3(0.15f, 0.15f, 1);
        if (BattlePhase == 1)
        {
            transform.position = rotationPoint.transform.position;
            Vector2 aimdirection = new Vector2(player.transform.position.x,player.transform.position.y) - rb.position;
            float aimangle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg + 90;
            rb.rotation = aimangle;
            if(canshootagain)
                StartCoroutine(Shoot());
        }
        if (phaseTrackScript.phasen	== 2)
            Destroy(transform.parent.gameObject);
        
    }
    public void StartRotate(GameObject phaseRotationPoint, int phase)
    {
        rotationPoint = phaseRotationPoint;
        BattlePhase = phase;
    }

    IEnumerator Shoot()
    {
        canshootagain = false;
        yield return new WaitForSeconds(Random.Range(2,5));
        GameObject bullet = Instantiate(NeekoProjectile, firePoint.transform.position, firePoint.transform.rotation * Quaternion.Euler(0, 0, 90));
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.transform.up * 2, ForceMode2D.Force);
        StartCoroutine(Shoot());
    }
}
