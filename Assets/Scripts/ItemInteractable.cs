using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : MonoBehaviour, IInteractable, IPickupable
{
    public string name;
    public float moveSpeed;
    public float stopFollowingRange;

    Transform playerTransform;
    public bool following;

    [Header("Materials")]
    public Material darkMaterial;
    public Material brightMaterial;
    private bool highlight;
    private SpriteRenderer sr;

    #region <<IInteractable>>
    public GameObject Interact(PlayerData playerData)
    {
        GetComponent<Collider>().enabled = false;
        following = true;
        return gameObject;
    }

    public bool CanInteract()
    {
        return !following;
    }

    public void ToggleHighlight()
    {
        //TODO
        Debug.Log("Toggle Highlight for " + gameObject.name);
        highlight = !highlight;
        if (highlight) {
            sr.material = brightMaterial;
        } else {
            sr.material = darkMaterial;
        }
    }
    #endregion

    #region <<IPickupable>>
    public string GetName()
    {
        return name;
    }

    public void Use(GameObject target)
    {
        Destroy(gameObject);
    }

    public void Drop()
    {
        GetComponent<Collider>().enabled = true;
        following = false;
    }

    public bool IsThrowable()
    {
        return false;
    }
    #endregion

    void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        highlight = false;
    }

    void Update()
    {
        if (following)
        {
            Vector3 vectorToPlayer = playerTransform.position - transform.position;
            float distanceFromPlayer = Mathf.Abs(vectorToPlayer.magnitude);
            if (distanceFromPlayer > stopFollowingRange)
            {
                transform.Translate(vectorToPlayer.normalized * moveSpeed * Time.deltaTime);
            }
        }
    }

    public bool IsDrinkable()
    {
        return false;
    }

    public void Drink()
    {
    }
}
