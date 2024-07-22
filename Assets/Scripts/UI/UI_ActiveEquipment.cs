using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

public class UI_ActiveEquipment : MonoBehaviour
{
    public ActiveEquipment currentEquipment;
    public Player player;
    public Image image;
    public GameObject cooldownOverlay;
    public TextMeshProUGUI timerText;


    void Awake()
    {
        player.OnEquipmentAdded += SetNewEquipment;
        player.OnEquipmentRemoved += RemoveEquipment;
    }

    void SetNewEquipment(GameObject newEquipment)
    {
        currentEquipment = newEquipment.GetComponent<ActiveEquipment>();
        player.onUseEquipment += StartItemUse;
        player.onStartEquipmentCooldown += StartCooldown;

        image.sprite = newEquipment.GetComponent<ActiveEquipment>().icon;
    }

    void RemoveEquipment(GameObject equipment)
    {
        currentEquipment = null;
        image.sprite = null;
    }

    void StartItemUse(int cooldownBuffer)
    {
        StartCoroutine(DoItemUse(cooldownBuffer));
    }

    IEnumerator DoItemUse(int cooldownBuffer)
    {
        image.color = new Color32(255, 255, 255, 127);
        yield return new WaitForSeconds(cooldownBuffer);
        image.color = new Color32(255, 255, 255, 255);
    }

    void StartCooldown(int cooldown)
    {
        StartCoroutine(DoCooldown(cooldown));
    }

    IEnumerator DoCooldown(int cooldown)
    {
        cooldownOverlay.SetActive(true);
        timerText.text = cooldown.ToString();
        while (cooldown > 0)
        {
            yield return new WaitForSeconds(1f);
            cooldown--;
            timerText.text = cooldown.ToString();
        }
        cooldownOverlay.SetActive(false);
    }


}
