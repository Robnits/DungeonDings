using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;
using Random = UnityEngine.Random;

public class chest : MonoBehaviour
{
    
    private List<string> commonItems = new List<string>();
    void Start()
    {
        ResetList();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        int hilf;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                TextMeshProUGUI buttonText1 = GameObject.Find("Button1").GetComponentInChildren<TextMeshProUGUI>();
                TextMeshProUGUI buttonText2 = GameObject.Find("Button2").GetComponentInChildren<TextMeshProUGUI>();
                TextMeshProUGUI buttonText3 = GameObject.Find("Button3").GetComponentInChildren<TextMeshProUGUI>();
                //buttonText.text = "hello World";
                
                
                    hilf = Random.Range(0, commonItems.Count);
                    buttonText1.text = commonItems[hilf];
                    commonItems.Remove(commonItems[hilf]);
                
            }
        }
    }

    private void ResetList()
    {
        commonItems.Clear();
        commonItems.Add("Minigun");
        commonItems.Add("Maschine Pistole");
        commonItems.Add("50.Cal");
        commonItems.Add("Marksmanrifle");
        commonItems.Add("Piercing 1");
        commonItems.Add("Piercing 2");
        commonItems.Add("Glass Cannon");
        commonItems.Add("Mamas Latschen");
        commonItems.Add("Dornen");
        commonItems.Add("Milch");
        commonItems.Add("Gewichte");
    }
}

