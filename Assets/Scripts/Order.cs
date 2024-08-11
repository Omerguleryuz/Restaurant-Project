using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public bool hasUnCookedBurger;
    public bool hasCookedBurger;
    public bool hasOverCookedBurger;
    public bool hasBun;

    Chef chef;

    private void Start()
    {
        chef = FindObjectOfType<Chef>();

        //Classic burger properties
        hasBun = true;
        hasCookedBurger = true;
    } 
}
