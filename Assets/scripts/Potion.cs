using UnityEngine;

public class Potion : MonoBehaviour
{
    private readonly float health = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            collision.GetComponent<player_Stats>().GetHealth(health);

            Destroy(gameObject);
        }
    }
}