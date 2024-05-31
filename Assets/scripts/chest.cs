using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using Random = UnityEngine.Random;

public class Chest : MonoBehaviour
{
    private UpgradeManager upgrademanager;
    private InputHandler inputHandler;
    private Animator anim;

    private bool isInRange = false;

    private void Start()
    {
        inputHandler = GameObject.Find("LevelLoader").GetComponent<InputHandler>();
        anim = GetComponent<Animator>();
        upgrademanager = GameObject.Find("SpawnerAndUpgradeHandler").GetComponent<UpgradeManager>();
    }

    private void Update()
    {
        if (inputHandler.IsPlayerInteracting() && isInRange)
            StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        anim.SetTrigger("Open");
        yield return new WaitForSeconds(0);
        upgrademanager.ItemRoll();
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            collision.gameObject.GetComponent<PlayerBehaviour>().SprechblasePressE(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            collision.gameObject.GetComponent<PlayerBehaviour>().SprechblasePressE(false);
        }
    }
}