using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitInteractable : MonoBehaviour, IInteractable {

    GameManager gm;

    public bool CanInteract() {
        return true;
    }

    public GameObject Interact(PlayerData playerData) {
        gm.SaveFriend(playerData.carryingFriend);
        playerData.carryingFriend = "";
        return gameObject;
    }

    public void ToggleHighlight() {
        //TODO
        Debug.Log("Toggle Highlight for " + gameObject.name);
    }

    void Awake() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
