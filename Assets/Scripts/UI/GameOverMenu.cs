using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{

    public GameObject exampleStat;
    public GameObject statPanel;
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ShowStats(Dictionary<string, int> stats)
    {
        foreach (Transform child in statPanel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (KeyValuePair<string, int> stat in stats)
        {
            GameObject newStat = Instantiate(exampleStat, statPanel.transform);
            newStat.GetComponent<TextMeshProUGUI>().text = stat.Key + ": " + stat.Value;
            newStat.transform.SetParent(statPanel.transform);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
