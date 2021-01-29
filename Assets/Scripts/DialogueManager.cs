using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour{
	Queue<DialogueEvent> eventQueue = new Queue<DialogueEvent>();
    Animator animator;
	
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
		animator = GetComponent<Animator>();
	}
}

public class DialogueEvent : ScriptableObject{
	public string content;
	public GameObject speaker;
	public string anmationTag;
}