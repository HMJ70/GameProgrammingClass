using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    public static event Action<int> OnCoinCollect;
    public int worth = 5;

    public void collect()
    {
        OnCoinCollect.Invoke(worth);
        sfxmanager.Play("Coin");
        Destroy(gameObject);
    }
}
