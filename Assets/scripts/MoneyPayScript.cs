using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPayScript : MonoBehaviour
{

    public Text moneyText;

    [SerializeField]
    private FloatSO scoreSO;

    private void Awake()
    {
        scoreSO.OldMoney += scoreSO.NewMoney;
        scoreSO.NewMoney = 0f;
        moneyText.text = scoreSO.OldMoney.ToString();
    }
    //changed das geld zu einer bestimmten zahl
    private void Changemoney(float money)
    {
    }
    public void AddMoney(float money)
    {
    }
    public void removeMoney(float money)
    {
    }
}
