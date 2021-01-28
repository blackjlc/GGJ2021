using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public int seconds;
    public int mins;

    public Color fontColor;

    public bool showMilliseconds;
    private float currentSeconds;
    private int timerDefault;

    // Start is called before the first frame update
    void Start()
    {
        timerText.color = fontColor;
        timerDefault = 0;
        timerDefault += (seconds + (mins * 60));
        currentSeconds = timerDefault;
    }

    // Update is called once per frame
    void Update()
    {
        if ((currentSeconds -= Time.deltaTime) <= 0)
        {
            TimeUp();
        }
        else
        {
            if (showMilliseconds)
                timerText.text = TimeSpan.FromSeconds(currentSeconds).ToString(@"mm\:ss\:fff");
            else
                timerText.text = TimeSpan.FromSeconds(currentSeconds).ToString(@"mm\:ss");
        }
    }

    private void TimeUp()
    {
        if (showMilliseconds)
            timerText.text = "00:00:000";
        else
            timerText.text = "00:00";
    }
}
