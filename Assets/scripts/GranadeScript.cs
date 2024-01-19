using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GranadeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public CircleCollider2D circleCollidertrigger;
    public Light2D light2D;

    public CircleCollider2D circleCollider;

    private void Start()
    {
        light2D.intensity = 0.1f;
        StartCoroutine(GranadeExplosion());
    }


    IEnumerator GranadeExplosion()
    {
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("IsExplodig");
        circleCollider.radius = 0;
        light2D.intensity = 1;
        circleCollidertrigger.radius = 0.2f;
        gameObject.transform.localScale = new Vector3(10,10,1);
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
