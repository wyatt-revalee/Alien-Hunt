using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDisplay : MonoBehaviour
{
    public Player player;

    public void Awake()
    {
        player.OnCoinChange += SetAmount;
    }
    public void SetAmount(int amount)
    {
        transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = amount.ToString();
    }
}
