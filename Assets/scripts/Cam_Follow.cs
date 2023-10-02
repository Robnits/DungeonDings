using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Follow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    private void FixedUpdate()
    {
        gameObject.transform.position = player.gameObject.transform.position;
    }
}
