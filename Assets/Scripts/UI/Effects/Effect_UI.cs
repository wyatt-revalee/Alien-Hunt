using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Effect_UI : MonoBehaviour
{
    public int stackSize = 1;
    public UnityEngine.UI.Image image;
    public TextMeshProUGUI stackText;

    public void AddToStack()
    {
        stackSize++;
        stackText.text = stackSize.ToString();
    }

    public void RemoveFromStack()
    {
        stackSize--;
        stackText.text = stackSize.ToString();
    }
}
