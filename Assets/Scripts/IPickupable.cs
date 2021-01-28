using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    string GetName();

    void Use();

    void Drop();

}
