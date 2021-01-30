using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityNPC : MonoBehaviour, IInteractable, IHittable
{

    //NPC
    public string name;
    public GameObject textBubblePrefab;
    public Vector3 textBubbleOffset;

    //Security
    public LayerMask targetLayer;
    public float lookRange;
    public float meleeRange;
    public float moveSpeed;

    public Transform targetTransform;
    public bool angry;
    public bool dead;

    private AnimationController anim;

    #region <<Interact>>
    public bool CanInteract()
    {
        return true;
    }

    public GameObject Interact(PlayerData playerData)
    {
        if (!dead)
        {
            if (transform.Find("TextBubble(Clone)") != null)
            {
                Destroy(transform.Find("TextBubble(Clone)").gameObject);
            }
            Debug.Log(gameObject.name + " says I am a Security.");
            GameObject go = Instantiate(textBubblePrefab, transform.position + textBubbleOffset, Quaternion.identity, transform);
            go.GetComponent<TextBubble>().Setup(gameObject.name + " says I am a Security.");
            Destroy(go, 6f);
        }
        return gameObject;
    }

    public void ToggleHighlight()
    {
        //TODO
        Debug.Log("Toggle Highlight for " + gameObject.name);
    }
    #endregion

    #region <<Hittable>>
    void HandleLook()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, lookRange, targetLayer);
        Debug.Log("Security sees " + colliders.Length + " targets");
        if (colliders.Length > 0)
        {
            targetTransform = colliders[0].transform;
        }
    }

    void HandleMove()
    {
        Vector3 dir = targetTransform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
        anim.Move(dir.x, 1);
    }

    public void Hit()
    {
        dead = true;
        anim.KnockedOut();

    }

    public bool IsDead()
    {
        return dead;
    }
    #endregion

    void Update()
    {
        if (!dead && angry)
        {
            if (targetTransform == null)
            {
                HandleLook();
            }
            else if (Vector3.Distance(transform.position, targetTransform.position) < meleeRange)
            {
                anim.Attack();
                targetTransform.gameObject.GetComponent<IHittable>()?.Hit();
            }
            else
            {
                HandleMove();
            }
        }
    }
}
