using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Friends
    public int totalFriendNum;
    int savedFriendNum;

    //Timer
    public float startTime;
    float timer;

    public void GameOver() {
        Debug.Log("Game Over");
    }

    public void Reset() {
        SceneManager.LoadScene(0);
        //EditorSceneManager.OpenScene();
    }

    public void Exit() {
        Application.Quit();
    }
}
