using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunCrate : MonoBehaviour, IInteractable
{
    [HideInInspector] public Hamburger currentHamburger;

    Chef chef;
    HamburgerHolder hamburgerHolder;

    private void Start()
    {
        chef = FindObjectOfType<Chef>();
        hamburgerHolder = FindObjectOfType<HamburgerHolder>();
    }

    public void Interact()
    {
        if (!chef.HasHamburger())
        {
            CreateHamburgerWithBun();
        }

        if (chef.currentHamburger.hasBun) return;

        //If chef has uncooked or overcooked burger, then don't do nothing
        if (chef.currentHamburger.hasCookedBurger)
        {
            //Chef grabs bun
            chef.currentHamburger.hasBun = true;

            //Activate hamburger plate in chef object

            //Chef holds hamburger plate
            chef.currentHamburger.DeActivateAllPlates();
            chef.currentHamburger.ActivateCurrentPlate();
        }
    }

    private void CreateHamburgerWithBun()
    {
        GameObject instance = Instantiate(chef.hamburgerPrefab, chef.hamburgerTransform.position, Quaternion.identity, chef.transform);
        hamburgerHolder.hamburgers.Add(instance.GetComponent<Hamburger>());

        chef.currentHamburger = instance.GetComponent<Hamburger>();
        instance.GetComponent<Hamburger>().AddBun();
    }
}
