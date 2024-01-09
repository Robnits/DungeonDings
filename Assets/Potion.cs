using UnityEngine;

public class Potion : MonoBehaviour
{
    private float health = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            collision.GetComponent<Player_Stats>().GetHealth(health);

            Destroy(gameObject);
        }
    }
}