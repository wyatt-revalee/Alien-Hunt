using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UI_Attribute : MonoBehaviour
{

    Attribute attributeInfo;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI valueText;

    public void SetAttributeInfo(Attribute infoToSet)
    {
        attributeInfo = infoToSet;
        nameText.text = attributeInfo.aName;
        valueText.text = attributeInfo.GetTrueValue().ToString();
        //Debug.Log(attributeInfo.GetTrueValue().ToString());
    }
}
