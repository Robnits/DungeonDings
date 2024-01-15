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
        scenemanager.StartSwitch(1);
    }
}
