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
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private MyPlayerInput inputs;
    public InputAction controlMap;
    public float speed;
    public float dashDurationSec = .5f;
    public float dashSpeed;
    private bool interruptInput = false;
    public bool enableControl = true;

    [Header("PlayerData")]
    public PlayerData playerData;

    [Header("Interact")]
    public float interactRange;
    public LayerMask interactableLayer;
    private IInteractable highlightedInteractable;

    [Header("Throw")]
    public float throwRange;
    private GameObject throwTarget;

    [Header("Animation")]
    private AnimationController anim;

    [Header("WalkEffects")]
    public GameObject walkEffect;
    public float effectTime;
    public Vector3 effectOffset;
    private float effectTimer;

    [Header("TextBubble")]
    public GameObject textBubblePrefab;
    public Vector3 textBubbleOffset;

    private DrunkController drunk;
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
        anim = GetComponent<AnimationController>();
        drunk = GetComponent<DrunkController>();
        effectTimer = effectTime;
        playerData = new PlayerData(anim);
    }

    private void Update()
    {
        if (!interruptInput)
        {
            Vector2 inputVector = controlMap.ReadValue<Vector2>();
            Vector3 finalVector = enableControl ? new Vector3(inputVector.x, 0, inputVector.y) : Vector3.zero;
            finalVector = drunk.GetDrunkQuaternion() * finalVector;
            anim.Move(finalVector.x, finalVector.sqrMagnitude > 0 ? 1 : 0);
            // Debug.Log(finalVector.ToString());
            controller.Move(finalVector * Time.deltaTime * speed);
            if (finalVector.magnitude != 0f)
            {
                HandleWalkEffect();
            }
        }
        if (playerData.item != null && playerData.item.IsThrowable())
        {
            HighlightThrow();
        }
        else
        {
            HighlightInteract();
        }
    }

    private void HandleInteract(InputAction.CallbackContext context)
    {
        GameObject go = null;
        if (playerData.item != null && playerData.item.IsThrowable() && throwTarget != null)
        {
            playerData.item.Use(throwTarget);
            playerData.item = null;
            throwTarget = null;
            highlightedInteractable.ToggleHighlight();
            highlightedInteractable = null;
        }
        else
        {
            go = highlightedInteractable?.Interact(playerData);
        }
        if (go != null && go.GetComponent<IPickupable>() != null)
        {
            if (playerData.item != null)
            {
                playerData.item.Drop();
            }
            playerData.item = go.GetComponent<IPickupable>();
        }
    }

    private void HighlightInteract()
    {
        Collider[] interactables = Physics.OverlapSphere(transform.position, interactRange, interactableLayer);
        if (interactables.Length <= 0 && highlightedInteractable != null)
        {
            highlightedInteractable.ToggleHighlight();
            highlightedInteractable = null;
        }
        else if (interactables.Length > 0)
        {
            IInteractable targetInteractable = interactables[0].GetComponent<IInteractable>();
            GameObject targetObject = interactables[0].gameObject;

            if (targetInteractable != highlightedInteractable && targetInteractable != null && targetInteractable.CanInteract())
            {
                if (highlightedInteractable != null)
                {
                    highlightedInteractable.ToggleHighlight();
                }
                highlightedInteractable = targetInteractable;
                highlightedInteractable.ToggleHighlight();
            }
        }
    }

    private void HighlightThrow()
    {
        Collider[] interactables = Physics.OverlapSphere(transform.position, throwRange, interactableLayer);
        if (interactables.Length <= 0 && highlightedInteractable != null)
        {
            highlightedInteractable.ToggleHighlight();
            highlightedInteractable = null;
        }
        if (interactables.Length <= 0 && throwTarget != null)
        {
            throwTarget = null;
        }
        else if (interactables.Length > 0)
        {
            IInteractable targetInteractable = null;
            GameObject targetObject = null;
            foreach (Collider collider in interactables)
            {
                if (collider.GetComponent<IHittable>() != null && !collider.GetComponent<IHittable>().IsDead())
                {
                    targetInteractable = collider.GetComponent<IInteractable>();
                    targetObject = collider.gameObject;
                }
            }
            if (targetInteractable != highlightedInteractable && targetInteractable != null && targetInteractable.CanInteract())
            {
                if (highlightedInteractable != null)
                {
                    highlightedInteractable.ToggleHighlight();
                }
                highlightedInteractable = targetInteractable;
                highlightedInteractable.ToggleHighlight();
                if (playerData.item != null && playerData.item.IsThrowable())
                {
                    throwTarget = targetObject;
                }
            }
        }
    }

    public void ShowTextBubble(string text)
    {
        if (transform.Find("TextBubble(Clone)") != null)
        {
            Destroy(transform.Find("TextBubble(Clone)").gameObject);
        }
        GameObject go = Instantiate(textBubblePrefab, transform.position + textBubbleOffset, Quaternion.identity, transform);
        go.GetComponent<TextBubble>().Setup(text);
        Destroy(go, 6f);
    }

    private void HandleWalkEffect()
    {
        if (effectTimer < 0)
        {
            Vector3 pos = transform.position + effectOffset;
            GameObject effect = Instantiate(walkEffect, pos, Quaternion.identity);
            Destroy(effect, .6f);
            effectTimer = effectTime;
        }
        else
        {
            effectTimer -= Time.deltaTime;
        }
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

            Vector2 secondMove;
            try
            {
                secondMove = await onMove.OnInvokeAsync<Vector2>(token).Timeout(TimeSpan.FromSeconds(.5f),
                                DelayType.Realtime, PlayerLoopTiming.FixedUpdate);
            }
            catch (System.Exception)
            {
                continue;
            }

            if (firstMove == secondMove)
            {
                print("dash");
                interruptInput = true;
                Vector3 finalVector = new Vector3(firstMove.x, 0, firstMove.y);
                float timer = dashDurationSec;
                while (!token.IsCancellationRequested && timer > 0)
                {
                    anim.Move(finalVector.x, 2);
                    controller.Move(finalVector * Time.deltaTime * dashSpeed);
                    timer -= Time.deltaTime;
                    await UniTask.NextFrame(cancellationToken: token);
                }
                // await UniTaskAsyncEnumerable.EveryUpdate().Take(60).ForEachAsync(_ =>
                // {
                //     anim.Move(finalVector.x, 2);
                //     controller.Move(finalVector * Time.deltaTime * dashSpeed);
                // });
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