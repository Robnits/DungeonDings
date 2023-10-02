using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    // Start is called before the first frame update
    
    private int raume;
    public GameObject[] toproom;
    public GameObject[] rightrooms;
    public GameObject[] botrooms;
    public GameObject[] leftrooms;
    public GameObject[] endrooms;


    public int begrenzung(){
            raume ++;
        return raume;
    }
}
