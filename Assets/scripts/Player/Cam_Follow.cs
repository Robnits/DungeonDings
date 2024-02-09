using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Cam_Follow : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private bool isShaking;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (!isShaking)
            gameObject.transform.position = player.transform.position;

    }
    //tutorial von Brackeys https://www.youtube.com/watch?v=9A9yj8KnM8c
    public IEnumerator CameraShake(float duration, float magnitude)
    {
        isShaking = true;
        
        float elapsed = 0f;
        while (elapsed < duration)
        {
            // Calculate a smooth random offset within the specified magnitude
            float x = player.transform.position.x + Random.Range(-0.1f, 0.1f) * magnitude;

            // Apply the accumulated offset to the camera's position
            transform.position = new Vector3(x, player.transform.position.y, player.transform.position.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = player.transform.position;
        isShaking = false;
    }
}
