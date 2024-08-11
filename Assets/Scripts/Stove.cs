using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stove : MonoBehaviour, IInteractable
{
    public GameObject pansParent;
    public GameObject panWithUnCookedBurger;
    public GameObject panWithCookedBurger;
    public GameObject panWithOverCookedBurger;

    [HideInInspector] public Hamburger currentHamburger;
    [HideInInspector] public float cookTime = 5f;
    [HideInInspector] public float overCookTime = 3f;
    [HideInInspector] public float pastCookTime = 0;
    [HideInInspector] public bool isBurnerFull;

    private Chef chef;
    private HamburgerHolder hamburgerHolder;
    private CookBar cookBar;
    

    private void Start()
    {
        chef = FindObjectOfType<Chef>();
        cookBar = GetComponent<CookBar>();
        hamburgerHolder = FindObjectOfType<HamburgerHolder>();
    }

    private void Update()
    {
        Cook();
    }


    public void Interact()
    {
        //Chef puts burger on stove
        if (chef.HasHamburger() == true && !chef.currentHamburger.hasBun && currentHamburger == null)
        {
            PutBurgerOnStove();
        }
        //Chef takes burger
        else if (currentHamburger != null)
        {
            if (chef.HasHamburger() == true)
            {
                if (chef.currentHamburger.hasBun)
                {
                    //Elimizde toplam 2 hamburger oluyor, düzelt
                }
            }
            else if (chef.HasHamburger() == false)
            {
                ChefGrabsBurger();
            }
        }
    }

    private void PutBurgerOnStove()
    {
        chef.currentHamburger.DeActivateAllPlates();  //Deactivate all plates of chef

        chef.currentHamburger.SetParent(transform);   //Set stove as new parent
        currentHamburger = chef.currentHamburger;     //Set current hamburger as the one that chef given  
        chef.currentHamburger = null;                 //Chef has no hamburger plates on his hand
        isBurnerFull = true;

        DeActivateAllPans();
        ActivateCorrespondingPan();
    }

    private void ActivateCorrespondingPan()
    {
        if (currentHamburger.hasUnCookedBurger) panWithUnCookedBurger.SetActive(true);
        else if (currentHamburger.hasCookedBurger) panWithCookedBurger.SetActive(true);
        else if (currentHamburger.hasOverCookedBurger) panWithOverCookedBurger.SetActive(true);
    }

    private void ChefGrabsBurger()
    {
        //Delivers hamburger to chef
        chef.currentHamburger = currentHamburger;
        currentHamburger.transform.parent = chef.hamburgerTransform;
        currentHamburger.ActivateCurrentPlate();

        //Stove has no hamburger on it
        currentHamburger = null;
        isBurnerFull = false;

        DeActivateAllPans();

        //Reset cook timer
        pastCookTime = 0;
        cookBar.cookBar.fillAmount = 0;
    }


    private void Cook()
    {
        if (!isBurnerFull) return;

        cookBar.FillBar();
    }

    public void BurgerCooked()
    {
        currentHamburger.hasCookedBurger = true;
        currentHamburger.hasUnCookedBurger = false;

        DeActivateAllPans();
        panWithCookedBurger.SetActive(true);
    }

    public void BurgerOverCooked()
    {
        currentHamburger.hasOverCookedBurger = true;
        currentHamburger.hasCookedBurger = false;
        currentHamburger.hasUnCookedBurger = false;

        DeActivateAllPans();
        panWithOverCookedBurger.SetActive(true);
    }

    private void DeActivateAllPans()
    {
        for (int i = 0; i < pansParent.transform.childCount; i++)
        {
            pansParent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
