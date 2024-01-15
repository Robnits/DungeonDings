using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauelenScript : MonoBehaviour
{
    private bool PlayerIsInRange;
    private bool AlreadyActive;
    private BossRoomScript bossRoomScript;
    private InputHandler inputHandler;
    private AudioSource audioSource;

    private void Start()
    {
        inputHandler = GameObject.Find("LevelLoader").GetComponent<InputHandler>();
        bossRoomScript = GameObject.Find("Haupttor").GetComponent<BossRoomScript>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerIsInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerIsInRange = false;
    }

    private void Update()
    {
        if (PlayerIsInRange && inputHandler.IsPLayerInteracting() && !AlreadyActive)
        {
            bossRoomScript.OpenGate();
            AlreadyActive = true;
            audioSource.Play();
        }
    }
}
