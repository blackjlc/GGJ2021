using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TMP_Text timeTxt;
    public float timer = 0.0f;

    public int seconds;
    public int minutes;
    public int hours;

    public Color fontColor;

    public bool showMilliseconds;
    private float currentSeconds;
    private int timerDefault;

    // Start is called before the first frame update
    void Start()
    {
        timerText.color = fontColor;
        timerDefault = 0;
        timerDefault += (seconds + (minutes * 60) + (hours * 60 * 60));
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
                timerText.text = TimeSpan.FromSeconds(currentSeconds).ToString(@"hh\:mm\:ss\:fff");
            else
                timerText.text = TimeSpan.FromSeconds(currentSeconds).ToString(@"hh\:mm\:ss");
        }
    }

    private void TimeUp()
    {
        if (showMilliseconds)
            timerText.text = "00:00:00:000";
        else
            timerText.text = "00:00:00";
    }

    void DosplayTimer()
    {
        minutes = Mathf.FloorToInt(timer / 60.0f);
        seconds = Mathf.FloorToInt(timer - minutes * 60);
        timeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
