using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour, IInteractable, IPickupable{

    //Item
    public string name;
    public float moveSpeed;
    public float stopFollowingRange;

    Transform playerTransform;
    public bool following;

    //Throwing
    public float throwSpeed;
	public float hitRange;
	public LayerMask hitLayer;
	Transform targetTransform;
	bool thrown = false;

    #region <<Interact>>
    public bool CanInteract() {
        return true;
    }

    public GameObject Interact(PlayerData playerData) {
        if (!following) {
            GetComponent<Collider>().enabled = false;
            following = true;
        } else if(playerData.target != null) {
            Use(playerData.target);
        }
        return gameObject;
    }

    public void ToggleHighlight() {
        //TODO
        Debug.Log("Toggle Highlight for " + gameObject.name);
    }
    #endregion

    #region <<IPickupable>>
    public string GetName() {
        return name;
    }

    public void Use(GameObject target) {
        targetTransform = target.transform;
        thrown = true;
        following = false;
    }

    public void Drop() {
        GetComponent<Collider>().enabled = true;
        following = false;
    }

    public bool IsThrowable() {
        return true;
    }
    #endregion


    private void HandleHit(){
		Collider[] colliders = Physics.OverlapSphere(transform.position, hitRange, hitLayer);
		foreach(Collider collider in colliders){
			if(collider.transform.Equals(targetTransform)){
				targetTransform.gameObject.GetComponent<IHittable>()?.Hit();
				Destroy(gameObject);
			}
		}
	}

    void Awake() {
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update(){
        if (following) {
            Vector3 vectorToPlayer = playerTransform.position - transform.position;
            float distanceFromPlayer = Mathf.Abs(vectorToPlayer.magnitude);
            if (distanceFromPlayer > stopFollowingRange) {
                transform.Translate(vectorToPlayer.normalized * moveSpeed * Time.deltaTime);
            }
        }else if (thrown){
			transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, throwSpeed * Time.deltaTime);
            transform.Rotate(new Vector3(0,0, 7));
            HandleHit();
		}
	}
}