using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawnerClose : MonoBehaviour
{
    private InputHandler inputHandler;
    private bool isInRange;
    private RatSpawner ratSpawner;
    private GameObject player;
    private AudioSource gullisound;
    // Start is called before the first frame update
    void Start()
    {
        gullisound = GetComponent<AudioSource>();
        ratSpawner = GetComponentInParent<RatSpawner>();
        inputHandler = GameObject.Find("LevelLoader").GetComponent<InputHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject;
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            player.GetComponent<PlayerBehaviour>().SprechblasePressE(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.gameObject;
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            player.GetComponent<PlayerBehaviour>().SprechblasePressE(false);
        }
    }

    private void Update()
    {
        if (isInRange && inputHandler.IsPlayerInteracting() && ratSpawner.ForChildIsOpen())
        {
            gullisound.Play();
            ratSpawner.IsOpenOrClosed(false);
        }  
        else if (isInRange && inputHandler.IsPlayerInteracting() && !ratSpawner.ForChildIsOpen())
        {
            gullisound.Play();
            ratSpawner.IsOpenOrClosed(true);
        }
            
        
    }

}
