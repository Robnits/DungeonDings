using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDevil : MonoBehaviour
{
    private bool playerIsInRange;
    private float Cooldown = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsInRange = true;
            //GetComponent<Devil>().playerIsInMelee = true;
            StartCoroutine(MeleeDevilCountdown());


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsInRange = false;
        }
    }

    IEnumerator MeleeDevilCountdown()
    {
        while(playerIsInRange)
        {
            yield return new WaitForSeconds(Cooldown);
        }
        yield return null;
    }
}
