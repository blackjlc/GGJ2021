using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : MonoBehaviour, IInteractable
{
    public string name;
    public float moveSpeed;
    public float stopFollowingRange;

    Transform playerTransform;
    public bool following;

    public GameObject Interact() {
        GetComponent<Collider>().enabled = false;
        following = true;
        return gameObject;
    }

    public bool CanInteract() {
        return !following;
    }

    public void ToggleHighlight() {
        Debug.Log("Toggle Highlight for " + gameObject.name);
    }

    public void Drop() {
        GetComponent<Collider>().enabled = true;
        following = false;
    }

    void Awake() {
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update() {
        if (following) {
            Vector3 vectorToPlayer = playerTransform.position - transform.position;
            float distanceFromPlayer = Mathf.Abs(vectorToPlayer.magnitude);
            if (distanceFromPlayer > stopFollowingRange) {
                transform.Translate(vectorToPlayer.normalized * moveSpeed * Time.deltaTime);
            }
        }
    }
}
