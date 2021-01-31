using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    public Transform door;
    public bool opened;

    [Header("Animation")]
    public float speed;
    public float range;

    [Header("Lock")]
    public string keyName;
    public bool locked;

    private AudioController sound;

    private void Start()
    {
        sound = GetComponent<AudioController>();
    }

    public GameObject Interact(PlayerData playerData)
    {
        Debug.Log(gameObject.name + " is interacted");
        if ((locked && playerData.item != null && playerData.item.GetName() == keyName))
        {//Locked Door
            IPickupable tmpItem = playerData.item;
            playerData.item = null;
            tmpItem.Use(null);
            locked = false;
            opened = !opened;

            sound.PlayOpenLock();
            sound.PlayDoorOpen();
        }
        else if (!locked)
        {//UnLocked Door
            opened = !opened;
            sound.PlayDoorOpen();
        }
        else // Unsuccess attempt to open locked door
        {
            sound.PlayDoorIsLocked();
        }
        return gameObject;
    }

    public bool CanInteract()
    {
        //return !locked;
        return true;
    }

    public void ToggleHighlight()
    {
        //TODO
        Debug.Log("Toggle Highlight for " + gameObject.name);
    }

    void Update()
    {
        if (opened && door.rotation.y < range)
        {
            door.Rotate(new Vector3(0, speed, 0) * Time.deltaTime);
        }
        else if (!opened && door.rotation.y > 0)
        {
            door.Rotate(new Vector3(0, -speed, 0) * Time.deltaTime);
        }
    }
}
