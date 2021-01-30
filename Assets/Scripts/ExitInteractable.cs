using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitInteractable : MonoBehaviour, IInteractable
{
    public float driveSpeed;
    public bool drive;
    GameManager gm;

    public bool CanInteract()
    {
        return true;
    }

    public GameObject Interact(PlayerData playerData)
    {
        if (gm.friendsName.Contains(playerData.carryingFriend))
        {
            gm.SaveFriend(playerData.carryingFriend);
            playerData.carryingFriend = "";
            playerData.animation.SetCarry(false);
        }
        return gameObject;
    }

    public void ToggleHighlight()
    {
        //TODO
        Debug.Log("Toggle Highlight for " + gameObject.name);
    }

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        if (drive && transform.position.x <100f) {
            transform.Translate(new Vector3(driveSpeed, 0, 0) * Time.deltaTime);
        }
    }
}
