using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MyPlayerInput inputs;
    public InputAction controlMap;
    public float speed;
    public float interactRange;

    public LayerMask interactableLayer;

    private CharacterController controller;
    private new Transform transform;

    void OnEnable()
    {
        inputs = new MyPlayerInput();
        inputs.Movement.Interact.performed += HandleInteract;
        inputs.Movement.Interact.Enable();
        controlMap.Enable();
    }

    void OnDisable()
    {
        inputs.Movement.Interact.performed -= HandleInteract;
        inputs.Movement.Interact.Disable();
        controlMap.Disable();
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        Vector2 inputVector = controlMap.ReadValue<Vector2>();
        Vector3 finalVector = new Vector3(inputVector.x, 0, inputVector.y);

        //Debug.Log(finalVector.ToString());
        controller.Move(finalVector * Time.deltaTime * speed);
    }

    private void HandleInteract(InputAction.CallbackContext obj)
    {
        Collider[] interactables = Physics.OverlapSphere(transform.position, 1, interactableLayer);
        interactables[0].GetComponent<IInteractable>().Interact();
    }
}
