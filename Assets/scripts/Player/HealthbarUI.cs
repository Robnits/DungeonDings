using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{

    [SerializeField] 
    private Slider sliderHealth;
    [SerializeField] 
    private Slider sliderDash;

    public void SetHealth(float health, float maxHealth)
    {
        sliderHealth.maxValue = maxHealth;
        sliderHealth.value = health;
    }

    public void SetDash(float dash, float dashmaxtime)
    {
        sliderDash.maxValue = dashmaxtime * 50;
        sliderDash.value = dash;
    }
}
