using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Cam_Follow : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        gameObject.transform.position = player.gameObject.transform.position;
    }
}
