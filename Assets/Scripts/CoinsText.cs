using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsText : MonoBehaviour
{
    public TextMeshProUGUI text;

    public int coins = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Increment()
    {
        coins++;
        text.SetText("$ x " + coins.ToString("00"));
    }
}
