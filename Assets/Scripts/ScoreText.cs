using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public int scoreValue = 0;
    public TextMeshProUGUI t;

    void Start()
    {
        t = GetComponent<TextMeshProUGUI>();
        addScore(0);
    }

    public void addScore(int x)
    {
        scoreValue += x;
        t.SetText("MARIO\n" + scoreValue.ToString("000000"));
    }
}
