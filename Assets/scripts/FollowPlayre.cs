using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayre : MonoBehaviour
{
    public GameObject player;
    Vector3 offset = new(0.7f, 0.4f, 0);
    

    void Update()
    {
        if (player != null)
            gameObject.transform.position = player.transform.position + offset;
    }
}
