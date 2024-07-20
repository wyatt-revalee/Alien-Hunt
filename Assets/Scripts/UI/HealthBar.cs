using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player player;
    public Slider slider;

    void Awake()
    {
        player.OnHealthChanged += UpdateHealth;
    }

    private void UpdateHealth(Attribute health)
    {
        slider.value = health.baseValue;
        slider.maxValue = health.max;
    }
}
