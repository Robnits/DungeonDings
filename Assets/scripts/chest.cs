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

    private bool isInRange = false;

    private void Start()
    {
        upgrademanager = GameObject.Find("SpawnerAndUpgradeHandler").GetComponent<UpgradeManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            upgrademanager.ItemRoll();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}