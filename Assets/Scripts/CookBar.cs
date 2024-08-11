using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookBar : MonoBehaviour
{
    public Image cookBar;
    private Stove stove;

    private void Start()
    {
        stove = GetComponent<Stove>();
    }

    public void FillBar()
    {
        //Cook
        if (stove.currentHamburger.hasUnCookedBurger)
        {
            BarCook();
        }
        //Over Cook
        else if (stove.currentHamburger.hasCookedBurger)
        {
            BarOverCook();
        }
        //Over Cooked
        else if (stove.currentHamburger.hasOverCookedBurger)
        {
            
        }
    }
    private void BarCook()
    {
        stove.pastCookTime += Time.deltaTime;

        cookBar.fillAmount = stove.pastCookTime / stove.cookTime;

        //Burger cooked
        if (cookBar.fillAmount == 1) 
        {
            stove.BurgerCooked();

            cookBar.fillAmount = 0;
            cookBar.color = Color.red;
            stove.pastCookTime = 0;
        }
    }

    private void BarOverCook()
    {
        stove.pastCookTime += Time.deltaTime;

        cookBar.fillAmount = stove.pastCookTime / stove.overCookTime;

        if (cookBar.fillAmount == 1) //Burger cooked
        {
            Debug.Log("Burger overcooked");
            stove.BurgerOverCooked();

            cookBar.fillAmount = 0;
            stove.pastCookTime = 0;
        }
    }

    
}
