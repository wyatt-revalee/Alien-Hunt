using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{

    public TextMeshProUGUI coins;
    public Player player;

    void Awake()
    {
        player.onCoinsChanged += UpdateCoins;
    }

    public void UpdateCoins(int coinAmount)
    {
        coins.text = coinAmount.ToString();
    }

}
