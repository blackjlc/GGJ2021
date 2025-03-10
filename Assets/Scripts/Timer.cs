﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public int seconds;
    public int minutes;
 //   public int hours;

  //  public Color fontColor;     

    public bool showMilliseconds;
    private float currentSeconds;
    private int timerDefault;
    private GameManager gm;
    private bool timeUp;

    private void Awake() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        timeUp = false;
    }

    void Start()
    {
     //   timeTxt.color = fontColor;
        timerDefault = 0;
        timerDefault += (seconds + (minutes * 60));
        currentSeconds = timerDefault;
    }

    // Update is called once per frame
    void Update()
    {

        countDown();


    }
    void countDown()
    {
        if (!timeUp) {
            if ((currentSeconds -= Time.deltaTime) <= 0) {
                TimeUp();
                gm.GameOver("The taxi has left without you...", true);
            } else {
                if (showMilliseconds)
                    timerText.text = TimeSpan.FromSeconds(currentSeconds).ToString(@"mm\:ss\:fff");
                else
                    timerText.text = TimeSpan.FromSeconds(currentSeconds).ToString(@"mm\:ss");
            }
        }
    }

    private void TimeUp()
    {
        timeUp = true;
        if (showMilliseconds)
            timerText.text = "00:00:000";
        else
            timerText.text = "00:00";
    }


}
