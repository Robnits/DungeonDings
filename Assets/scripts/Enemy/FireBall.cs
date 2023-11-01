using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
   
    private float speed = 10f;
    public Rigidbody2D FireBallRB;
    


    private void Start()
    {
        FireBallRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
        DestoyAfterTime();
    }

    void DestoyAfterTime()
        {
        StartCoroutine(DestroyAfterTime());
        }
   
        private void OnCollisionEnter2D(Collision2D collision)
        {
        if (collision.gameObject != CompareTag("Enemy"))
        {
            Destroy(gameObject);
            GetComponent<DevilFireBall>().FireBallActive = false;
        }

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
