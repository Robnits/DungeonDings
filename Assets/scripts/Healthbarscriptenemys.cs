using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Healthbarscriptenemys : MonoBehaviour
{
    public GameObject healthbar;

    private float maxhealth;

    private void Start()
    {
        healthbar.SetActive(false);
    }
    public void GetMaxhealth(float maxlife)
    {
        maxhealth = maxlife;
    }
    
    public void FollowEnemy(Vector3 position)
    {
        healthbar.transform.position = position + new Vector3(0f, 1f, 0f);
    }
    
    public void GetDamaged(float life)
    {
        healthbar.SetActive (true);
        gameObject.transform.localScale = new Vector3(life / maxhealth, 1f, 1f);
    }
}
