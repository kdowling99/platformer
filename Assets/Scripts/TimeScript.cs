using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    public float timeValue = 100;
    public TextMeshProUGUI t;
    public bool countdown = true;
    public GameObject mario;

    void Start()
    {
        t = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (timeValue - Time.deltaTime > 0 && countdown)
        {
            timeValue = timeValue - Time.deltaTime;
        }
        else if (countdown)
        {
            timeValue = 0;
            mario.GetComponent<CharacterController>().killMario();
        }

        t.SetText("TIME\n" + Mathf.FloorToInt(timeValue).ToString("000"));
    }

    public void stopCountdown()
    {
        countdown = false;
    }
}