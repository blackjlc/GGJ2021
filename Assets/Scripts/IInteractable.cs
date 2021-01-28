using UnityEngine;
public interface IInteractable
{
    GameObject Interact();

    bool CanInteract();

    void ToggleHighlight();
}
