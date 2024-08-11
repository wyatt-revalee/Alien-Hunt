using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public GameObject statPrefab;
    public GameObject statPanel;

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void DisplayStats(Dictionary<string, Stat> stats)
    {
        foreach(KeyValuePair<string, Stat> kvp in stats)
        {
            Debug.Log(string.Format("{0} : {1}", kvp.Key, kvp.Value.total));
            GameObject newUIStat = Instantiate(statPrefab, statPanel.transform);
            Stat newStat = new Stat();
            newStat.total = kvp.Value.total;
            newUIStat.GetComponent<UI_Stat>().SetStatInfo(kvp.Key, newStat);
        }
    }

}
