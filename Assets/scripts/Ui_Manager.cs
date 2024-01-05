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

    private float healthCurrentprice = 1;
    private float damageCurrentprice = 1;

    private const float multiplier = 1.7f;

    private void Awake()
    {
        SetUI();
    }

    public void Upgrade_Life()
    {
        healthCurrentprice = multiplier * GlobalVariables.healthUpgrade;
        if (GlobalVariables.money >= healthCurrentprice && GlobalVariables.healthUpgrade <= 10)
        {
            GlobalVariables.money -= healthCurrentprice;
            GlobalVariables.healthUpgrade ++;
        }
        SetUI();
    }
    public void Upgrade_Damage()
    {
        damageCurrentprice = multiplier * GlobalVariables.damageUpgrade;
        if(GlobalVariables.money >= damageCurrentprice && GlobalVariables.damageUpgrade <= 10)
        {
            GlobalVariables.money -= damageCurrentprice;
            GlobalVariables.damageUpgrade ++;
        }
        SetUI();
    }
    private void SetUI()
    {
        healthCurrentprice = multiplier * GlobalVariables.healthUpgrade;
        damageCurrentprice = multiplier * GlobalVariables.damageUpgrade;

        btnText_Life.text = (healthCurrentprice).ToString() + "$";
        btnText_Damage.text = (damageCurrentprice).ToString() + "$";

        Text_Life.text = (GlobalVariables.healthUpgrade - 1).ToString() + "/10";
        Text_Damage.text = (GlobalVariables.damageUpgrade - 1).ToString() + "/10";

        moneyText.text = GlobalVariables.money.ToString() + "$";
    }
}
