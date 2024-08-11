using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWall : MonoBehaviour, IInteractable
{
    public List<Order> orders;
    Hamburger currentHamburger;
    Chef chef;
    CustomerOrderManager customerOrderManager;

    private void Start()
    {
        chef = FindObjectOfType<Chef>();
        customerOrderManager = FindObjectOfType<CustomerOrderManager>();
    }
    public void Interact()
    {
        customerOrderManager.GivePreparedOrderToCustomer();
    }
}
