using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBoss : MonoBehaviour
{
    public GameObject player;
    Vector3 offset = new(1.3f, 0.63f, 0);
    
    void Update()
    {
        if (player != null)
            gameObject.transform.position = player.transform.position + offset;
    }
}
