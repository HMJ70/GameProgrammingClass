using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour,IItem
{
    public int healamount = 1;
    public static event Action<int> onhealthcollect;

    private bool collected = false;
    public void collect()
    {
        if (collected) return;
        collected = true;


        onhealthcollect.Invoke(healamount);

        Destroy(gameObject);
    }
}
