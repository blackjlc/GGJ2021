using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;

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

    // Audio
    public bool startMusic = true;
    public DrunkController drunk;
    public AudioController DJ;

    public AudioController ambient;
    private CancellationToken token;

    public void SaveFriend(string friend)
    {
        friendsName.Remove(friend);
        savedFriendNum++;
        drunk.Drink();
        dm.UpdateFriendText(friendsName);
        if (totalFriendNum == savedFriendNum)
        {
            Win();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void Win()
    {
        Debug.Log("You found all your friends.\n Congrats!");
        dm.friendText.gameObject.SetActive(false);
        dm.ShowBlackScreen("You found all your friends.\n Congrats!");
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Awake()
    {
        dm = GetComponent<DialogueManager>();
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
