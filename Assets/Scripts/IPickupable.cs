using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    bool IsDrinkable();
    void Drink();

    string GetName();

    bool IsThrowable();

    void Use(GameObject gameObject);

    void Drop();

}
