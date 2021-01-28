using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{
    private MyPlayerInput inputs;
    public InputAction controlMap;
    public float speed;
    public float interactRange;
    public LayerMask interactableLayer;
    public Animator animator;
    private int moveAniID;
    private int moveXAniID;
    private int moveSpeedAniID;
    private int carryAniID;
    private int pickKeyAniID;
    private int danceAniID;

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
        Assert.IsNotNull(animator, "Animator should not be null!!");
        moveAniID = Animator.StringToHash("move");
        moveXAniID = Animator.StringToHash("move_x");
        moveSpeedAniID = Animator.StringToHash("move_speed");
        carryAniID = Animator.StringToHash("carry");
        pickKeyAniID = Animator.StringToHash("pick_key");
        danceAniID = Animator.StringToHash("dance");
    }

    private void Update()
    {
        Vector2 inputVector = controlMap.ReadValue<Vector2>();
        Vector3 finalVector = new Vector3(inputVector.x, 0, inputVector.y);

        // print(finalVector);
        HandleAnimation(finalVector.x, finalVector.sqrMagnitude);

        //Debug.Log(finalVector.ToString());
        controller.Move(finalVector * Time.deltaTime * speed);
    }

    private void HandleInteract(InputAction.CallbackContext obj)
    {
        Collider[] interactables = Physics.OverlapSphere(transform.position, interactRange, interactableLayer);
        interactables[0].GetComponent<IInteractable>().Interact();
    }

    private void HandleAnimation(float speedX, float speed)
    {
        animator.SetFloat(moveXAniID, speedX > 0 ? 1f : -1f);
        animator.SetFloat(moveSpeedAniID, speed > 0 ? 1f : 0f);
    }
}
