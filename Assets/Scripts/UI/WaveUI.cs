using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public WaveController waveController;
    public TextMeshProUGUI cornerWaveText;

    void Start()
    {
        waveController.OnWaveStarted += NewWaveStarted;
        cornerWaveText.text = "Wave " + waveController.currentWave.ToString();
    }

    void NewWaveStarted(int waveNum)
    {
        Debug.Log("Setting new wave text");
        cornerWaveText.text = "Wave " + waveNum.ToString();
    }
}
