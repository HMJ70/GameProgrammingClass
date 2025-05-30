using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressamount;
    public Slider progresslider;

    void Start()
    {
        progressamount = 0;
        progresslider.value = 0;
        Coin.OnCoinCollect += IncreaseProgressAmount;
    }

    private Action<int> IncreaseProgressAmount()
    {
        throw new NotImplementedException();
    }

    void IncreaseProgressAmount(int amount)
    {
        progressamount += amount;
        progresslider.value = progressamount;
        if(progressamount >= 100)
        {
            Debug.Log("levelcomplete");
        }

    }

    void Update()
    {
        
    }
}
