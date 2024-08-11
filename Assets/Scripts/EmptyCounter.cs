using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCounter : MonoBehaviour, IInteractable
{
    public GameObject platesParent;
    public GameObject unCookedBurgerPlate;
    public GameObject cookedBurgerPlate;
    public GameObject overCookedBurgerPlate;
    public GameObject bunPlate;
    public GameObject hamburgerPlate;

    Hamburger currentHamburger;
    Chef chef;
    

    private void Start()
    {
        chef = FindObjectOfType<Chef>();
    }
    public void Interact()
    {
        //If chef has a plate
        if (chef.HasHamburger())
        {
            PutPlateOnTheCounter();
        }
        else
        {
            GrabPlate();
        }
    }

    private void PutPlateOnTheCounter()
    {
        chef.currentHamburger.DeActivateAllPlates();    //Deactivate all plates of chef
        chef.currentHamburger.SetParent(transform);     //Set empty counter as new parent
        currentHamburger = chef.currentHamburger;       //Set current hamburger as the one that chef given  

        chef.currentHamburger = null;                   //Chef has no hamburger plates on his hand

        ActivateCorrespondingPlate();
    }

    private void GrabPlate()
    {
        chef.currentHamburger = currentHamburger;
        currentHamburger.transform.parent = chef.hamburgerTransform;
        currentHamburger = null;

        chef.currentHamburger.ActivateCurrentPlate();
        chef.currentHamburger.transform.localPosition = Vector3.zero;

        DeActivateAllPlates();
    }

    private void ActivateCorrespondingPlate()
    {
        if (currentHamburger.hasUnCookedBurger) unCookedBurgerPlate.SetActive(true);
        else if (currentHamburger.hasCookedBurger && !currentHamburger.hasBun) cookedBurgerPlate.SetActive(true);
        else if (currentHamburger.hasOverCookedBurger) overCookedBurgerPlate.SetActive(true);
        else if (currentHamburger.hasBun && !currentHamburger.hasCookedBurger) bunPlate.SetActive(true);
        else if (currentHamburger.hasBun && currentHamburger.hasCookedBurger) hamburgerPlate.SetActive(true);
    }

    private void DeActivateAllPlates()
    {
        for (int i = 0; i < platesParent.transform.childCount; i++)
        {
            platesParent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
