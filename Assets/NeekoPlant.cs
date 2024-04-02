using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeekoPlant : MonoBehaviour
{
    [SerializeField]
    private List<Transform> firepoints; // Changed to use Transform instead of GameObject
    
    [SerializeField]
    private GameObject featherProjectile;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThrowLeaf());
        Debug.Log("Number of fire points: " + firepoints.Count);
    }

    IEnumerator ThrowLeaf()
    {
        yield return new WaitForSeconds(5);
        foreach (Transform firepoint in firepoints)
        {
            GameObject bullet = Instantiate(featherProjectile, firepoint.position, firepoint.rotation * Quaternion.Euler(0, 0, 90));
            bullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * 2, ForceMode2D.Impulse); // Changed to ForceMode2D.Impulse
        }
        StartCoroutine(ThrowLeaf());
    }
}
