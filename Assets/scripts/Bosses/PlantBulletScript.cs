using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBulletScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
        else if (other.gameObject.CompareTag("Player"))
            StartCoroutine(DestroyAfterTime());
    }
    private IEnumerator DestroyAfterTime(){
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }
}
