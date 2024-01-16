using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{

    public Slider sliderHealth;
    public Slider sliderDash;

    public void SetHealth(float health, float maxHealth)
    {
        sliderHealth.maxValue = maxHealth;
        sliderHealth.value = health;
    }

    public void SetDash(float dash, float dashmaxtime)
    {
        sliderDash.maxValue = dashmaxtime * 100;
        sliderDash.value = dash;
    }
}
