using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    public GameObject hamburgerPrefab;
    public Transform hamburgerTransform;
    
    [HideInInspector] public Hamburger currentHamburger;

    public void InteractWith(IInteractable interactable)
    {
        interactable.Interact();
    }

    public bool HasHamburger()
    {
        return currentHamburger;
    }
}
