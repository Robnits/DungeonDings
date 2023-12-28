using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField]
    private AudioSource auswahl;
    
    public void Auswahl()
    {
        auswahl.Play();
    }
}
