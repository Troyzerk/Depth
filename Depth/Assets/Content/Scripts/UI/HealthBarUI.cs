using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthSlider;
    public Slider fullBarSlider;

    public void SetHealth(float health)
    {
        fullBarSlider.value = health;
    }

    public void SetMaxHealth(float health)
    {
        fullBarSlider.maxValue = health;
        healthSlider.value = health;
    }

}
