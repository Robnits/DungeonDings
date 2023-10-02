using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Manager : MonoBehaviour
{
    public Text btnText_Life;
    public Text btnText_Damage;
    public Text Text_Life;
    public Text Text_Damage;
    public Text moneyText;

    private float multiplier = 1.7f;

    [SerializeField]
    private UpgradeSO upgradeSO;

    [SerializeField]
    private FloatSO scoreSO;

    private void Awake()
    {
        btnText_Life.text = (1 + multiplier * upgradeSO.Life).ToString() + "$";
        btnText_Damage.text = (1 + multiplier * upgradeSO.Damage).ToString() + "$";
        Text_Damage.text = upgradeSO.Damage.ToString() + "/25 Damage";
        Text_Life.text = upgradeSO.Life.ToString() + "/10 Life";
        scoreSO.OldMoney += scoreSO.NewMoney;
        scoreSO.NewMoney = 0f;
        moneyText.text = scoreSO.OldMoney.ToString() + "$";
    }

    public void Upgrade_Life()
    {
        if (scoreSO.OldMoney >= multiplier * upgradeSO.Life +1f && upgradeSO.Life < 10)
        {
           
            scoreSO.OldMoney -= multiplier * upgradeSO.Life;
            upgradeSO.Life++;
            moneyText.text = scoreSO.OldMoney.ToString() + "$";
            Text_Life.text = upgradeSO.Life.ToString() + "/10 Life";
            btnText_Life.text = (1 + multiplier * upgradeSO.Life).ToString() + "$";
        }
        else
        {
            Debug.Log("you cannot buy this right now");
        }
    }
    public void Upgrade_Damage()
    {
        if (scoreSO.OldMoney >= multiplier * upgradeSO.Damage + 1f && upgradeSO.Damage <25)
        {
            
            scoreSO.OldMoney -= multiplier * upgradeSO.Damage;
            upgradeSO.Damage++;
            moneyText.text = scoreSO.OldMoney.ToString() + "$";
            Text_Damage.text = upgradeSO.Damage.ToString() + "/25 Damage";
            btnText_Damage.text = (1 + multiplier * upgradeSO.Damage).ToString() + "$";
        }
        else
        {
            Debug.Log("you cannot buy this right now");
        }
    }


    
}
