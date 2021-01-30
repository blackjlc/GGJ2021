using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityAnimationListener : MonoBehaviour
{
    SecurityNPC npcBehaviour;
    public void AttackEvent() {
        npcBehaviour.Attack();
    }

    private void Awake() {
        npcBehaviour = transform.parent.gameObject.GetComponent<SecurityNPC>();
    }
}
