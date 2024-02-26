using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{


    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TextMeshProUGUI healthText;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);

        healthText.text = string.Format("{0}/{1}", slider.value, slider.maxValue);
    }


    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
        healthText.text = string.Format("{0}/{1}", slider.value, slider.maxValue);
    }

}
