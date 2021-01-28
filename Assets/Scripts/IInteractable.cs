using UnityEngine;
public interface IInteractable
{
    GameObject Interact(PlayerData playerData);

    bool CanInteract();

    void ToggleHighlight();
}
