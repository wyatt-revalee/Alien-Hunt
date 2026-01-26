using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointDrop : MonoBehaviour
{

    public TextMeshProUGUI pointDropText;

    public PointDrop(string value)
    {
        pointDropText.text = value;
    }

    void Start()
    {
        StartCoroutine(PointDropEffect());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(PointDropEffect());
        }
    }

    public IEnumerator PointDropEffect()
    {
        yield return new WaitForSeconds(0.5f);
        while(pointDropText.color.a > 0.0f)
        {
            pointDropText.color = new Color(pointDropText.color.r, pointDropText.color.g, pointDropText.color.b, pointDropText.color.a - (Time.deltaTime / 1));
            yield return null;
        }
        Destroy(gameObject);
        Destroy(this);
    }

}
