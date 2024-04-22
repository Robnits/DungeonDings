using System.Collections;
using UnityEngine;

public class phase2NeekoBullet : MonoBehaviour
{
    private readonly float speed = 4f;
    private Rigidbody2D FireBallRB;
    private void Start()
    {
        FireBallRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Move the feather in a straight line
        FireBallRB.velocity = transform.up * speed;
        

        StartCoroutine(DestroyAfterTime());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer != 6 && collision.gameObject.layer != 9)
            StartCoroutine(DestroyAfterDamage());
    }
    IEnumerator DestroyAfterDamage(){
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}