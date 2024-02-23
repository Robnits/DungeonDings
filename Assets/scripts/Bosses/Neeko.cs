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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (phaseTrackScript == null)
            phaseTrackScript = GameObject.Find("PhaseTracker").GetComponent<PhaseTrackScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        life = 100f;
        damage = 5;
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
                transform.position = rotationPoint.transform.position;
                Vector2 aimdirection = new Vector2(player.transform.position.x,player.transform.position.y) - rb.position;
                float aimangle = Mathf.Atan2(aimdirection.y, aimdirection.x) * Mathf.Rad2Deg + 90;
                rb.rotation = aimangle;
                break;
        }
    }

    public void StartRotate(GameObject phaseRotationPoint)
    {
        
        rotationPoint = phaseRotationPoint;
        BattlePhase++;
    }


    public void shoot()
    {
        

    }
}
