﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;

public class GameManager : MonoBehaviour
{
    //Friends
    public CutsceneHandler ch;
    public int totalFriendNum;
    public List<string> friendsName;
    private int savedFriendNum;
    private List<SecurityNPC> allSecurity;
    private DialogueManager dm;

    //Timer
    public float startTime;
    float timer;

    // Audio
    public bool startMusic = true;
    public DrunkController drunk;
    public AudioController DJ;
    public void AddSecurity(SecurityNPC s) {
        allSecurity.Add(s);
    }

    public AudioController ambient;
    private CancellationToken token;
    public void TriggerAllSecurity() {
        foreach(SecurityNPC security in allSecurity) {
            security.angry = true;
        }
    }

    public void ResetAllSecurity() {
        foreach (SecurityNPC security in allSecurity) {
            security.angry = false;
        }
    }

    public void SaveFriend(string friend) {
        friendsName.Remove(friend);
        savedFriendNum++;
        drunk.Drink();
        dm.UpdateFriendText(friendsName);
        if (totalFriendNum == savedFriendNum)
        {
            Win();
        }
    }

    public void GameOver(string msg, bool showScene) {
        Debug.Log(msg);
        ResetAllSecurity();
        if (showScene) {
            StartCoroutine(ch.EndScene(msg));
        } else {
            StartCoroutine(ch.SimpleEndScene(msg));
        }
    }

    public void Win()
    {
        Debug.Log("You found all your friends.\n Congrats!");
        //dm.friendText.gameObject.SetActive(false);
        //dm.ShowBlackScreen("You found all your friends.\n Congrats!");
        GameOver("You found all your friends.\n Congrats!", true);
    }

    public void Reset() {
        RetryManager.Retry();
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Awake()
    {
        dm = GetComponent<DialogueManager>();
        allSecurity = new List<SecurityNPC>();
        savedFriendNum = 0;
    }

    private void Start()
    {
        dm.UpdateFriendText(friendsName);
        token = this.GetCancellationTokenOnDestroy();
        if (startMusic)
        {
            StartMusic(token).Forget();
            ambient.PlayAmbience();
        }
    }

    private async UniTaskVoid StartMusic(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            DJ.PlayMusic1();
            await UniTask.Delay(16000);
            DJ.PlayMusic2();
            await UniTask.Delay(74000);
        }
    }

    [ContextMenu("Stop Music")]
    public void StopMusic()
    {
        DJ.Stop();
        ambient.Stop();
    }
}
