using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDrink : MonoBehaviour, IItem
{
    public static event Action<float> onspeedcollected;
    public float speedmultiplier =  1.5f;

    private bool collected = false;
    public void collect()
    {
        if (collected) return;
        collected = true;

        onspeedcollected.Invoke(speedmultiplier);
        Destroy(gameObject);
    }


}
