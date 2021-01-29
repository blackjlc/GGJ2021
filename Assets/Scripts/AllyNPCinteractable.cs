using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyNPCInteractable : MonoBehaviour, IInteractable {

    //public QuickTimeEvent qte;

    public bool CanInteract() {
        return true;
    }

    public GameObject Interact(PlayerData playerData){
        // start qte or item given
        //if success
        // return object or event
        // else show fail message
        return gameObject;
	}

    public void ToggleHighlight() {
        //TODO
        Debug.Log("Toggle Highlight for " + gameObject.name);
    }
}