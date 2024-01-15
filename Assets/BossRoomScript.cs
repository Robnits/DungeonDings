using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomScript : MonoBehaviour
{

    private int SaeulenCount;
    // Start is called before the first frame update
    void Start()
    {
        SaeulenCount = 0;
        gameObject.transform.position = new Vector3 (40.5f, -83.5f, 0);
    }

    public void OpenGate()
    {
        SaeulenCount++;
        if (SaeulenCount == 3)
            GateOpen();
    }

    private void GateOpen()
    {
        gameObject.transform.position = new Vector3(40.5f, -81, 0);
    }
}
