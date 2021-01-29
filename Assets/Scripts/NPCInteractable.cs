using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour,IInteractable
{
    public string npcName;
    public GameObject textBubblePrefab;
    public Vector3 textBubbleOffset;
    public bool isFriend;

    public bool CanInteract() {
        return true;
    }

    public GameObject Interact(PlayerData playerData) {
        Debug.Log(gameObject.name + " says Hi.");
        Debug.Log("Carrying " + playerData.carryingFriend);
        if (!isFriend) {
            if(transform.Find("TextBubble(Clone)") != null) {
                Destroy(transform.Find("TextBubble(Clone)").gameObject);
            }
            GameObject go = Instantiate(textBubblePrefab, transform.position + textBubbleOffset, Quaternion.identity, transform);
            go.GetComponent<TextBubble>().Setup(gameObject.name + " says Hi.");
            Destroy(go, 6f);
            return gameObject;
        } else if(playerData.carryingFriend == null || playerData.carryingFriend.Length <= 0 ) {
            playerData.carryingFriend = npcName;
            gameObject.SetActive(false);
            return null;
        }
        return gameObject;
    }

    public void ToggleHighlight() {
        //TODO
        Debug.Log("Toggle Highlight for " + gameObject.name);
    }

}
