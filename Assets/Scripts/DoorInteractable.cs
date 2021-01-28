using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    public Transform door;
    public bool opened;
    public float speed;
    public float range;
    public bool locked;

    public GameObject Interact()
    {
        opened = !opened;
        return gameObject;
    }

    public bool CanInteract() {
        return !locked;
    }

    public void ToggleHighlight() {
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
