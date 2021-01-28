using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Events;
using Cysharp.Threading.Tasks.Linq;

public class PlayerController : MonoBehaviour
{
    private MyPlayerInput inputs;
    public InputAction controlMap;
    public float speed;
    public int dashDurationFrame = 60;
    public float dashSpeed;
    private bool interruptInput = false;

    // Interact
    public float interactRange;
    public LayerMask interactableLayer;

    // Animation
    private bool isRight = true;
    public Animator animator;
    private int moveAniID;
    private int moveXAniID;
    private int moveSpeedAniID;
    private int carryAniID;
    private int pickKeyAniID;
    private int danceAniID;

    private CharacterController controller;
    private new Transform transform;
    private MoveEvent onMove;
    private CancellationTokenSource tokenSource;

    void OnEnable()
    {
        inputs = new MyPlayerInput();
        inputs.Movement.Interact.performed += HandleInteract;
        controlMap.performed += OnMove;
        inputs.Movement.Interact.Enable();
        controlMap.Enable();
        ListenDashInput(tokenSource.Token).Forget();
    }



    void OnDisable()
    {
        tokenSource.Cancel();
        inputs.Movement.Interact.performed -= HandleInteract;
        controlMap.performed -= OnMove;
        inputs.Movement.Interact.Disable();
        controlMap.Disable();
    }

    private void Awake()
    {
        tokenSource = new CancellationTokenSource();
        onMove = new MoveEvent();
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
        if (!interruptInput)
        {
            Vector2 inputVector = controlMap.ReadValue<Vector2>();
            Vector3 finalVector = new Vector3(inputVector.x, 0, inputVector.y);
            HandleAnimation(finalVector.x, finalVector.sqrMagnitude > 0 ? 1 : 0);
            // Debug.Log(finalVector.ToString());
            controller.Move(finalVector * Time.deltaTime * speed);
        }
    }

    private void HandleInteract(InputAction.CallbackContext context)
    {
        Collider[] interactables = Physics.OverlapSphere(transform.position, interactRange, interactableLayer);
        interactables[0].GetComponent<IInteractable>().Interact();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="speedX"></param>
    /// <param name="speed">0 for idle. 1 for walk. 2 for dash</param>
    private void HandleAnimation(float speedX, float speed)
    {
        if (speedX != 0)
            isRight = speedX > 0;
        animator.SetFloat(moveXAniID, isRight ? 1f : -1f);
        animator.SetFloat(moveSpeedAniID, speed);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        onMove.Invoke(context.ReadValue<Vector2>());
    }

    private async UniTaskVoid ListenDashInput(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            Vector2 firstMove = await onMove.OnInvokeAsync<Vector2>(token);
            Vector2 secondMove = await onMove.OnInvokeAsync<Vector2>(token);
            if (firstMove == secondMove)
            {
                print("dash");
                interruptInput = true;
                Vector3 finalVector = new Vector3(firstMove.x, 0, firstMove.y);
                await UniTaskAsyncEnumerable.EveryUpdate().Take(60).ForEachAsync(_ =>
                {
                    HandleAnimation(finalVector.x, 2);
                    controller.Move(finalVector * Time.deltaTime * dashSpeed);
                });
                interruptInput = false;
            }
        }
    }

    private void OnDestroy()
    {
        tokenSource.Dispose();
    }
}



[System.Serializable]
public class MoveEvent : UnityEvent<Vector2>
{
}