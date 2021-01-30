using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void AddSecurity(SecurityNPC s) {
        allSecurity.Add(s);
    }

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
        dm.UpdateFriendText(friendsName);
        if(totalFriendNum == savedFriendNum) {
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

    public void Win() {
        Debug.Log("You found all your friends.\n Congrats!");
        //dm.friendText.gameObject.SetActive(false);
        //dm.ShowBlackScreen("You found all your friends.\n Congrats!");
        GameOver("You found all your friends.\n Congrats!", true);
    }

    public void Reset() {
        RetryManager.Retry();
        SceneManager.LoadScene(0);
    }

    public void Exit() {
        Application.Quit();
    }

    private void Awake() {
        dm = GetComponent<DialogueManager>();
        allSecurity = new List<SecurityNPC>();
        savedFriendNum = 0;
    }

    private void Start() {
        dm.UpdateFriendText(friendsName);
    }
}
