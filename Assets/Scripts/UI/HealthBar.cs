using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player player;
    public Slider slider;
    public TextMeshProUGUI text;

    void Awake()
    {
        player.OnHealthChanged += UpdateHealth;
    }

    private void UpdateHealth(Attribute health)
    {
        slider.maxValue = health.max;
        slider.value = health.baseValue;

        text.text = string.Format("{0}/{1}", health.baseValue, health.max);
    }
}
