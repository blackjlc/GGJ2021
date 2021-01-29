using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Friends
    public int totalFriendNum;
    public List<string> friendsName;
    int savedFriendNum;

    DialogueManager dm;

    //Timer
    public float startTime;
    float timer;

    public void SaveFriend(string friend) {
        friendsName.Remove(friend);
        savedFriendNum++;
        dm.UpdateFriendText(friendsName);
        if(totalFriendNum == savedFriendNum) {
            Win();
        }
    }

    public void GameOver() {
        Debug.Log("Game Over");
    }

    public void Win() {
        Debug.Log("You found all your friends.\n Congrats!");
        dm.friendText.gameObject.SetActive(false);
        dm.ShowBlackScreen("You found all your friends.\n Congrats!");
    }

    public void Reset() {
        SceneManager.LoadScene(0);
    }

    public void Exit() {
        Application.Quit();
    }

    private void Awake() {
        dm = GetComponent<DialogueManager>();
        savedFriendNum = 0;
    }

    private void Start() {
        dm.UpdateFriendText(friendsName);
    }
}
