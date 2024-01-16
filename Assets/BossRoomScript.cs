using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossRoomScript : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    private int SaeulenCount;
    // Start is called before the first frame update
    void Start()
    {
        SaeulenCount = 0;
        textMeshProUGUI.text = SaeulenCount.ToString() + " / 3 Pillars";
        gameObject.transform.position = new Vector3 (40.5f, -83.5f, 0);
    }

    public void OpenGate()
    {
        SaeulenCount++;
        textMeshProUGUI.text = SaeulenCount.ToString() + " / 3 Pillars";
        if (SaeulenCount == 3)
            GateOpen();
    }

    private void GateOpen()
    {
        gameObject.transform.position = new Vector3(40.5f, -81, 0);
    }
}
