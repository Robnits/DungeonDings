using System.Collections;
using UnityEngine;

public class FeatherNeeko : MonoBehaviour
{
    private GameObject player;
    private readonly float speed = 2f;
    private Rigidbody2D FireBallRB;
    private readonly float damage = 30f;
    private DealDamageToPlayer ddtp;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        Vector2 directionToPlayer = player.transform.position - transform.position;

        // Calculate the angle to rotate towards the player
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90;

        // Rotate the feather to face the player
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        FireBallRB = GetComponent<Rigidbody2D>();
        ddtp = GetComponent<DealDamageToPlayer>();
        ddtp.dmg = damage;
    }

    private void Update()
    {
        // Move the feather in a straight line
        FireBallRB.velocity = transform.up * speed;
        

        StartCoroutine(DestroyAfterTime());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}