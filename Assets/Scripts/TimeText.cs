using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    public float timeValue = 500;
    public TextMeshProUGUI t;

    void Start()
    {
        t = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (timeValue - Time.deltaTime > 0)
        {
            timeValue = timeValue - Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            //murder mario
        }

        t.SetText("TIME\n" + Mathf.FloorToInt(timeValue).ToString("000"));
    }
}