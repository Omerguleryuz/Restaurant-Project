using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncookedBurgerCrate : MonoBehaviour, IInteractable
{
    private Chef chef;
    private HamburgerHolder hamburgerHolder;

    private void Start()
    {
        chef = FindObjectOfType<Chef>();
        hamburgerHolder = FindObjectOfType<HamburgerHolder>();
    }

    public void Interact()
    {
        if (chef.HasHamburger() == false)
        {
            CreateHamburgerWithUncookedBurger();
        }
    }

    private void CreateHamburgerWithUncookedBurger()
    {
        GameObject instance = Instantiate(chef.hamburgerPrefab, chef.hamburgerTransform.position, Quaternion.identity, chef.transform);
        hamburgerHolder.hamburgers.Add(instance.GetComponent<Hamburger>());

        chef.currentHamburger = instance.GetComponent<Hamburger>();
        instance.GetComponent<Hamburger>().AddUnCookedBurger();
    }
}
