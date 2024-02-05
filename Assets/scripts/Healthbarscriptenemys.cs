using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Healthbarscriptenemys : MonoBehaviour
{
    public GameObject healthbar;
    public GameObject enemy;

    private float maxhealth;

    private void Start()
    {
        healthbar.SetActive(false);
    }
    public void GetMaxhealth(float maxlife)
    {
        maxhealth = maxlife;
    }
    
    private void Update()
    {
        if (healthbar != null)
            healthbar.transform.position = enemy.transform.position + new Vector3(0f, 0.8f, 0f);
    }

    public void GetDamaged(float life)
    {
        healthbar.SetActive (true);
        gameObject.transform.localScale = new Vector3(life / maxhealth, 1f, 1f);
    }
}
