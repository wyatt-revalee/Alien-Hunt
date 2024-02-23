using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointPopup : MonoBehaviour
{

    public int value;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();
        Animate();
    }

    public void SetValue(int enemyValue)
    {
        value = enemyValue;
    }

    public void Animate()
    {
        StartCoroutine(DisplayAndDestroy());
    }

    public IEnumerator DisplayAndDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
