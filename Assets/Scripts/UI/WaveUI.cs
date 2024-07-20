using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public WaveController waveController;
    public TextMeshProUGUI cornerWaveText;
    public GameObject waveEndPanel;
    public TextMeshProUGUI waveEndMainText;
    public TextMeshProUGUI waveEndInfoText;

    public GameObject waveStartPanel;
    public TextMeshProUGUI waveCountdownText;

    void Awake()
    {
        waveController.OnWaveStarted += NewWaveStarted;
        waveController.OnwaveEnded += WaveEnded;
        cornerWaveText.text = "Wave " + waveController.currentWave.ToString();
    }

    void WaveEnded(int waveNum)
    {
        waveEndMainText.text = string.Format("Wave {0} complete", waveNum);
        SetWaveInfoText("");
        waveEndPanel.SetActive(true);
    }

    public void SetWaveInfoText(string infoText)
    {
        waveEndInfoText.text = infoText;
    }

    public void StartWaveCountdown(int seconds)
    {
        waveStartPanel.SetActive(true);
        StartCoroutine(NewWaveCountdown(seconds));
    }

    public IEnumerator NewWaveCountdown(int seconds)
    {
        while(seconds > 0)
        {
            waveCountdownText.text = "Next wave starting in...\n" + seconds.ToString();
            yield return new WaitForSeconds(1f);
            seconds--;
        }
        waveStartPanel.SetActive(false);
    }

    void NewWaveStarted(int waveNum)
    {
        //Debug.Log("Setting new wave text");
        cornerWaveText.text = "Wave " + waveNum.ToString();
    }

    public void HidePanel()
    {
        waveEndPanel.SetActive(false);
    }

}
