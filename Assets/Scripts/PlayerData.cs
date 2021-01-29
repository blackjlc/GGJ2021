using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public IPickupable item;
    public GameObject target;
    public string carryingFriend;
    public AnimationController animation;

    public PlayerData(AnimationController animation)
    {
        this.animation = animation;
        item = null;
        target = null;
        carryingFriend = null;
    }
}
