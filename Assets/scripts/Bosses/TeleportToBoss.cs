using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToBoss : MonoBehaviour
{

    private Scenemanager scenemanager;
    private void Start()
    {
        scenemanager = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Scenemanager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GlobalVariables.isInBossFight = true;
        scenemanager.StartSwitch(3);
    }
}
