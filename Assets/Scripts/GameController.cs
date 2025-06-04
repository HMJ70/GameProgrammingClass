using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
    private int completedLevels = 0;

    public AudioSource BGM;

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
        Player.GetComponent<BETTERMOVEMENT>().enabled = false;
        Time.timeScale = 0;

        if (BGM != null)
        {
            BGM.Stop();  
        }
        sfxmanager.Play("GameOver");
    }

    public void RetryGame()
    {
        completedLevels = 0;
        gameoverscreen2.SetActive(false);
        loadlevel(0,false);
        onreset.Invoke();
        Player.GetComponent<BETTERMOVEMENT>().enabled = true;
        Time.timeScale = 1;

        if (BGM  != null && !BGM.isPlaying)
        {
            BGM.Play();  
        }

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
        completedLevels++;

        if (completedLevels >= 4)
        {
            SceneManager.LoadScene("Victory"); 
            return;
        }

        int nextlevelindex = (currlevelindex == levels.Count - 1) ? 0 : currlevelindex + 1;
        loadcanvas.SetActive(false);

        levels[currlevelindex].SetActive(false);
        levels[nextlevelindex].SetActive(true);

        Player.transform.position = new Vector3(-13, 8, 0);

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
