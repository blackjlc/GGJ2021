using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour,IInteractable
{
    public string name;

    public bool CanInteract() {
        return true;
    }

    public GameObject Interact(PlayerData playerData) {
        Debug.Log(gameObject.name + " says Hi.");
        return gameObject;
    }

    public void ToggleHighlight() {
        //TODO
        Debug.Log("Toggle Highlight for " + gameObject.name);
    }

}
