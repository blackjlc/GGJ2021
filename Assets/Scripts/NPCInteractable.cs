using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    public string npcName;
    public GameObject textBubblePrefab;
    public Vector3 textBubbleOffset;
    public string dialogue;
    public bool isFriend;

    [Header("Materials")]
    public Material darkMaterial;
    public Material brightMaterial;
    private bool highlight;
    [SerializeField] private SpriteRenderer[] renderer;

    private void Start()
    {
        renderer = transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>();
    }

    public bool CanInteract()
    {
        return true;
    }

    public GameObject Interact(PlayerData playerData)
    {
        Debug.Log(gameObject.name + " says " + dialogue);
        Debug.Log("Carrying " + playerData.carryingFriend);
        if (!isFriend)
        {
            if (transform.Find("TextBubble(Clone)") != null)
            {
                Destroy(transform.Find("TextBubble(Clone)").gameObject);
            }
            GameObject go = Instantiate(textBubblePrefab, transform.position + textBubbleOffset, Quaternion.identity, transform);
            go.GetComponent<TextBubble>().Setup(dialogue);
            Destroy(go, 6f);
            return gameObject;
        }
        else if (playerData.carryingFriend == null || playerData.carryingFriend.Length <= 0)
        {
            playerData.carryingFriend = npcName;
            playerData.animation.SetCarry(true);
            gameObject.SetActive(false);
            return null;
        }
        return gameObject;
    }

    public void ToggleHighlight()
    {
        //TODO
        Debug.Log("Toggle Highlight for " + gameObject.name);
        highlight = !highlight;
        if (highlight)
        {
            for (int i = 0; i < renderer.Length; i++)
            {
                renderer[i].material.SetFloat("_OutlineOpacity", 1);
            }
        }
        else
        {
            for (int i = 0; i < renderer.Length; i++)
            {
                renderer[i].material.SetFloat("_OutlineOpacity", 0);
            }
        }
    }

}
