using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EbeneTextScript : MonoBehaviour
{
    private GameObject player;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player != null)
        {
            if (player.transform.position.x <= -500)
                textMeshPro.text = "Ebene 2";
            else if (player.transform.position.x >= 500)
                textMeshPro.text = "Ebene 3";
            else
                textMeshPro.text = "Ebene 1";
        }
    }
}
