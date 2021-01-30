using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour{

    public CanvasGroup blackScreen;
    public CanvasGroup cinematic;
    public GameObject blackBackGround;
    public Text blackScreenText;
    public Text friendText;
    public bool isBlackout;
    public bool isCinematic;
    public float fadeSpeed;

    GameObject player;
	Queue<DialogueEvent> eventQueue = new Queue<DialogueEvent>();

    public void UpdateFriendText(List<string> friendList) {
        string stringBuffer = "Missing Friends: ";
        foreach(string friend in friendList) {
            stringBuffer += friend + "  ";
        }
        friendText.text = stringBuffer;
    }

    IEnumerator BlackScreenPopText(string text) {
        string stringBuffer = "";
        foreach (char letter in text.ToCharArray()) {
            stringBuffer += letter;
            blackScreenText.text = stringBuffer;
            yield return new WaitForSeconds(0.07f);
        }
    }

    public void DisablePlayer() {
        player.GetComponent<PlayerController>().enableControl = false;
    }

    public void ShowBlackScreen(string text) {
        //friendText.gameObject.SetActive(false);
        isBlackout = true;
        StartCoroutine(BlackScreenPopText(text));
    }

    public void HideBlackScreen() {
        isBlackout = false;
    }

    public void ShowFriendCounter() {
        friendText.gameObject.SetActive(true);
    }

    public void StartDialogue(){
		//disable player control
		//animate screen
	}
	
	public void EndDialogue(){
		//enable player control
		//animate screen
	}
	
	public void NextEvent(){
		
	}
	
	public void ShowTransitionScreen(float duration, string text){
		
	}
	
	void Awake(){
        //player = GameObject.Find("Player");

    }

    private void Start() {
        blackScreen.alpha = 0;
        cinematic.alpha = 0;
    }

    private void Update() {
        if(isBlackout && blackScreen.alpha < 1) {
            blackScreen.alpha += fadeSpeed * Time.deltaTime;
        }else if (!isBlackout && blackScreen.alpha > 0) {
            blackScreen.alpha -= fadeSpeed * Time.deltaTime;
        }
        if (isCinematic && cinematic.alpha < 1) {
            cinematic.alpha += fadeSpeed * Time.deltaTime;
        } else if (!isCinematic && cinematic.alpha > 0) {
            cinematic.alpha -= fadeSpeed * Time.deltaTime;
        }
    }
}

public class DialogueEvent : ScriptableObject{
	public string content;
	public GameObject speaker;
	public string anmationTag;
}