using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public IPickupable item;
    public GameObject target;
    public string carryingFriend;

    public PlayerData() {
        item = null;
        target = null;
        carryingFriend = null;
    }
}
