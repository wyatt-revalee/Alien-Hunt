using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
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
    }
}
