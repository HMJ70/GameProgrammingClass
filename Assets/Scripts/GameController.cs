using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressamount;
    public Slider progresslider;

    public GameObject Player;
    public GameObject loadcanvas;
    public List<GameObject> levels;

    private int currlevelindex = 0;


    public GameObject gameoverscreen2;
    public static event Action onreset;

    void Start()
    {
        progressamount = 0;
        progresslider.value = 0;
        Coin.OnCoinCollect += IncreaseProgressAmount;
        PlayerHealth.ondeath += gameoverscreen;
        gameoverscreen2.SetActive(false);

        HoldToLoad.onholdcomplete += loadnextlevel;

        loadcanvas.SetActive(false);

    }

  

    void gameoverscreen()
    {
        gameoverscreen2.SetActive(true);
        Time.timeScale = 0;
    }

    public void RetryGame()
    {
        gameoverscreen2.SetActive(false);
        loadlevel(0,false);
        onreset.Invoke();
        Time.timeScale = 1;
       
    }
    void loadlevel(int level, bool wantsurvivedicrease)
    {
        loadcanvas.SetActive(false);

        levels[currlevelindex].gameObject.SetActive(false);
        levels[level].gameObject.SetActive(true);

        Player.transform.position = new Vector3(-13, -8, 0);

        currlevelindex = level;
        progressamount = 0;
        progresslider.value = 0;
    }

    void loadnextlevel()
    {
        int nextlevelindex = (currlevelindex == levels.Count - 1) ? 0 : currlevelindex + 1;
        loadcanvas.SetActive(false);

        levels[currlevelindex].gameObject.SetActive(false);
        levels[nextlevelindex].gameObject.SetActive(true);

        Player.transform.position = new Vector3(0, 0, 0);

        currlevelindex = nextlevelindex;
        progressamount = 0;
        progresslider.value = 0;
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
            loadcanvas.SetActive(true);
        }

    }

    void Update()
    {
        
    }
}
