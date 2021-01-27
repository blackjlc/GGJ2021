using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    public Transform door;
    public bool opened;
    public float speed;
    public float range;

    public void Interact() {
        opened = !opened;
    }
    
    void Update()
    {
        if (opened && door.rotation.y < range) {
            door.Rotate(new Vector3(0, speed, 0) * Time.deltaTime);
        } else if (!opened && door.rotation.y > 0) {
            door.Rotate(new Vector3(0, -speed, 0) * Time.deltaTime);
        }
    }
}
