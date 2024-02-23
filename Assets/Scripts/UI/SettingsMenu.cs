using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public WeaponController weaponController;
    public GameObject redSlider;
    public GameObject greenSlider;
    public GameObject blueSlider;
    public GameObject exampleCrosshair;

    public void Close()
    {
        // Open settings menu
        pauseMenu.SetActive(true);
        gameObject.SetActive(false);

    }

    public void ChangeCrosshairColor()
    {
        float redValue = redSlider.GetComponentInChildren<Slider>().value / 255f;
        float greenValue = greenSlider.GetComponentInChildren<Slider>().value / 255f;
        float blueValue = blueSlider.GetComponentInChildren<Slider>().value / 255f;

        Debug.Log(string.Format("Changing crosshair color {0} {1} {2}", redValue, greenValue, blueValue));
        // Change crosshair color
        exampleCrosshair.GetComponent<Image>().color = new Color(redValue, greenValue, blueValue);
        weaponController.currentWeaponScript.ChangeCrosshairColor(new Color(redValue, greenValue, blueValue));
        weaponController.crosshairColor = new Color(redValue, greenValue, blueValue);

        redSlider.GetComponent<TextMeshProUGUI>().text = string.Format("Red ({0})", (int)(redValue*255));
        greenSlider.GetComponent<TextMeshProUGUI>().text = string.Format("Green ({0})", (int)(greenValue * 255));
        blueSlider.GetComponent<TextMeshProUGUI>().text = string.Format("Blue ({0})", (int)(blueValue * 255));
    }
}
