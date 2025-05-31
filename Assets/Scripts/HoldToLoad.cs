using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HoldToLoad : MonoBehaviour
{
    public float holdduration = 1f;
    public Image fillcircle;

    private float holdtimer = 0; 
    private bool isholding = false;

    public static event Action onholdcomplete;


    void Update()
    {
        if(isholding)
        {
            holdtimer += Time.deltaTime;
            fillcircle.fillAmount = holdtimer / holdduration;

            if(holdtimer >= holdduration)
            {
                onholdcomplete.Invoke();
                resethold();
            }

        }
    }

    public void  onhold(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            isholding = true;
        }
        else if (context.canceled)
        {
            resethold();
        }
    }

    private void resethold()
    {
        isholding = false ;
        holdtimer = 0;
        fillcircle.fillAmount = 0;
    }


}
