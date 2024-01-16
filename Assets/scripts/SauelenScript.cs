using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SauelenScript : MonoBehaviour
{
    private bool PlayerIsInRange;
    private bool AlreadyActive;
    private BossRoomScript bossRoomScript;
    private InputHandler inputHandler;
    private AudioSource audioSource;
    private CanvasGroup canvasGroup;
    private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        canvasGroup = GameObject.FindGameObjectWithTag("ui").GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        textMeshProUGUI = canvasGroup.GetComponentInChildren<TextMeshProUGUI>();
        inputHandler = GameObject.Find("LevelLoader").GetComponent<InputHandler>();
        bossRoomScript = GameObject.Find("Haupttor").GetComponent<BossRoomScript>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            PlayerIsInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            PlayerIsInRange = false;
    }

    private void Update()
    {
        if (PlayerIsInRange && inputHandler.IsPLayerInteracting() && !AlreadyActive)
        {
            StartCoroutine(ShowText());
            bossRoomScript.OpenGate();
            AlreadyActive = true;
            audioSource.Play();
        }
    }

    IEnumerator ShowText()
    {
        textMeshProUGUI.text = "Pillar Activated";
        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(1);
        canvasGroup.alpha = 0;

    }
}
