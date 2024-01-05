using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSpawnerClose : MonoBehaviour
{
    private InputHandler inputHandler;
    private bool isInRange;
    private RatSpawner ratSpawner;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        ratSpawner = GetComponentInParent<RatSpawner>();
        inputHandler = GameObject.Find("LevelLoader").GetComponent<InputHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject;
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            player.GetComponent<Player_behjaviour>().CloseGulli(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.gameObject;
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            player.GetComponent<Player_behjaviour>().CloseGulli(false);
        }
    }

    private void Update()
    {
        if (isInRange && inputHandler.IsPLayerInteracting() && ratSpawner.ForChildIsOpen())
            ratSpawner.IsOpenOrClosed(false);
        else if (isInRange && inputHandler.IsPLayerInteracting() && !ratSpawner.ForChildIsOpen())
            ratSpawner.IsOpenOrClosed(true);
        
    }

}
