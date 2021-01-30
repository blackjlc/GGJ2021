using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState { Start, Player, End}

public class CutsceneHandler : MonoBehaviour
{
    private DialogueManager dm;
    private PlayerController playerController;
    private Animator animator;
    public bool skip;

    public void SwitchCamera(CameraState state) {
        switch (state) {
            case CameraState.Start:
                animator.Play("StartSceneCamera");
                break;
            case CameraState.Player:
                animator.Play("PlayerCamera");
                break;
            case CameraState.End:
                animator.Play("EndSceneCamera");
                break;
            default:
                animator.Play("PlayerCamera");
                break;
        }
    }

    IEnumerator StartScene() {
        dm.blackBackGround.SetActive(true);
        SwitchCamera(CameraState.Start);
        dm.isCinematic = true;
        playerController.enableControl = false;
        dm.ShowBlackScreen("It's time to go home. The taxi is waiting.");
        yield return new WaitForSeconds(5.5f);
        dm.blackBackGround.SetActive(false);
        dm.HideBlackScreen();
        yield return new WaitForSeconds(1.5f);
        playerController.ShowTextBubble("Guys, it's time to go.");
        yield return new WaitForSeconds(3.5f);
        playerController.ShowTextBubble("Wait, where are they?");
        yield return new WaitForSeconds(3.5f);
        dm.ShowBlackScreen("Let's find them.");
        yield return new WaitForSeconds(4.5f);
        dm.HideBlackScreen();
        yield return new WaitForSeconds(1.5f);
        SwitchCamera(CameraState.Player);
        dm.isCinematic = false;
        playerController.enableControl = true;
        //Start timer
    }

    public IEnumerator EndScene(string msg) {
        dm.ShowBlackScreen("");
        dm.isCinematic = true;
        dm.DisablePlayer();
        yield return new WaitForSeconds(2f);
        GameObject.Find("Player").SetActive(false);
        SwitchCamera(CameraState.End);
        dm.HideBlackScreen();
        GameObject.Find("Taxi").GetComponent<ExitInteractable>().drive = true;
        yield return new WaitForSeconds(2f);
        dm.ShowBlackScreen("msg");
        yield return new WaitForSeconds(4.5f);
        dm.ShowButtons();
    }

    public IEnumerator SimpleEndScene(string msg) {
        dm.DisablePlayer();
        dm.ShowBlackScreen("msg");
        yield return new WaitForSeconds(4.5f);
        dm.ShowButtons();
    }

    void StartWithoutCutscene() {
        SwitchCamera(CameraState.Player);
        dm.isCinematic = false;
        playerController.enableControl = true;
        dm.blackBackGround.SetActive(false);
        dm.HideBlackScreen();
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        dm = GameObject.Find("GameManager").GetComponent<DialogueManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Start()
    {
        skip = RetryManager.hasRetried();

        if (skip) {
            StartWithoutCutscene();
        } else {
            StartCoroutine(StartScene());
        }
    }
}
