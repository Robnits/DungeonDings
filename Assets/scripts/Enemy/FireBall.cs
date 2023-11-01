using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private GameObject player;
    private float speed = 3f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 1f)
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));

        }
    }

    void DestoyAfterTime()
        {
        StartCoroutine(DestroyAfterTime());
        }
   
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
            GetComponent<DevilFireBall>().FireBallActive = false;

        }
        

        IEnumerator DestroyAfterTime()
        {
        
        if(GetComponent<DevilFireBall>().FireBallActive == true)
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
            GetComponent<DevilFireBall>().FireBallActive = false;
        }
            yield return null;

        }
}
