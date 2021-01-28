using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour,IInteractable
{
    public string name;
    public GameObject textBubblePrefab;
    public Vector3 textBubbleOffset;

    public bool CanInteract() {
        return true;
    }

    public GameObject Interact(PlayerData playerData) {
        Debug.Log(gameObject.name + " says Hi.");
        GameObject go = Instantiate(textBubblePrefab, transform.position + textBubbleOffset, Quaternion.identity, transform);
        go.GetComponent<TextBubble>().Setup(gameObject.name + " says Hi.");
        Destroy(go, 3f);
        return gameObject;
    }

    public void ToggleHighlight() {
        //TODO
        Debug.Log("Toggle Highlight for " + gameObject.name);
    }

}
