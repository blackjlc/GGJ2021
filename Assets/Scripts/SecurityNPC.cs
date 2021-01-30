using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityNPC : MonoBehaviour, IInteractable, IHittable
{

    //NPC
    public string name;
    public GameObject textBubblePrefab;
    public Vector3 textBubbleOffset;
    public string dialogue;

    //Security
    public LayerMask targetLayer;
    public float lookRange;
    public float meleeRange;
    public float moveSpeed;

    public Transform targetTransform;
    public bool angry;
    public bool dead;

    private AnimationController anim;
    private AudioController sound;
    private GameManager gm;

    private void Awake()
    {
        anim = GetComponent<AnimationController>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        sound = GetComponent<AudioController>();
        anim = GetComponent<AnimationController>();
        gm.AddSecurity(this);
    }

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
            Debug.Log(gameObject.name + " says " + dialogue);
            GameObject go = Instantiate(textBubblePrefab, transform.position + textBubbleOffset, Quaternion.identity, transform);
            go.GetComponent<TextBubble>().Setup(dialogue);
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
        //Debug.Log("Security sees " + colliders.Length + " targets");
        if (colliders.Length > 0)
        {
            targetTransform = colliders[0].transform;
        }
    }

    void HandleMove()
    {
        Vector3 dir = targetTransform.position - transform.position + new Vector3(Random.Range(-1,1),0, Random.Range(-1, 1));
        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, moveSpeed * Time.deltaTime);
        anim.Move(dir.x, 1);
    }

    public void Hit()
    {
        sound.PlayBottleCrash();
        dead = true;
        anim.KnockedOut();
        gm.TriggerAllSecurity();
    }

    public bool IsDead()
    {
        return dead;
    }
    #endregion

    #region <<Melee>>
    public void Attack() {
        Debug.Log(gameObject.name + " Attacked");
        if (Vector3.Distance(transform.position, targetTransform.position) < meleeRange * 2) {
            if (!targetTransform.gameObject.GetComponent<IHittable>().IsDead()) {
                targetTransform.gameObject.GetComponent<IHittable>().Hit();
            }
        }
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
            }
            else
            {
                HandleMove();
            }
        }
    }
}
