using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Stat : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI valueText;

    public void SetStatInfo(string name, Stat statInfo)
    {
        nameText.text = name;
        valueText.text = statInfo.current.ToString();
    }
}
