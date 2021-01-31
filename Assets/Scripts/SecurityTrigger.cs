using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityTrigger : MonoBehaviour
{
    public float detectRange;
    public LayerMask playerLayer;
    private GameManager gm;

    private void Awake() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRange, playerLayer);
        if(colliders.Length > 0) {
            gm.TriggerAllSecurity();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
