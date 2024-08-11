using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hamburger : MonoBehaviour
{
    public GameObject unCookedBurgerPlate;
    public GameObject cookedBurgerPlate;
    public GameObject overCookedBurgerPlate;
    public GameObject bunPlate;
    public GameObject hamburgerPlate;

    public bool hasUnCookedBurger;
    public bool hasCookedBurger;
    public bool hasOverCookedBurger;
    public bool hasBun;

    public void AddUnCookedBurger()
    {
        DeActivateAllPlates();

        hasUnCookedBurger = true;
        unCookedBurgerPlate.SetActive(true);
    }
    public void AddBun()
    {
        DeActivateAllPlates();

        hasBun = true;
        bunPlate.SetActive(true);
    }

    public void ActivateCurrentPlate()
    {
        //Uncooked burger plate
        if (hasUnCookedBurger)
        {
            unCookedBurgerPlate.SetActive(true);
        }
        //Cooked burger plate
        else if (hasCookedBurger && !hasBun)
        {
            cookedBurgerPlate.SetActive(true);
        }
        //Overcooked burger plate
        else if (hasOverCookedBurger)
        {
            overCookedBurgerPlate.SetActive(true);
        }
        //Hamburger plate
        else if (hasCookedBurger && hasBun)
        {
            hamburgerPlate.SetActive(true);
        }
        //Bun Plate
        else if (hasBun)
        {
            bunPlate.SetActive(true);
        }
    }

    public void DeActivateAllPlates()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void SetParent(Transform newParent)
    {
        transform.parent = newParent;
    }
}
