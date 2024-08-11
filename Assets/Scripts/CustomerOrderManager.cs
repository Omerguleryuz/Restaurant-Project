using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrderManager : MonoBehaviour
{
    [HideInInspector] public OrderType orderType;
    [SerializeField] Sprite orderSprite;
    [SerializeField] GameObject orderPrefab;
    Hamburger orderableBurger;
    Chef chef;
    Order order;
    CustomerWall customerWall;
    GoldManager goldManager;
    UIManager uiManager;
    CustomerSpawnManager customerSpawnManager;

    private void Start()
    {
        chef = FindObjectOfType<Chef>();
        customerWall = FindObjectOfType<CustomerWall>();
        goldManager = FindObjectOfType<GoldManager>();
        uiManager = FindObjectOfType<UIManager>();
        customerSpawnManager = FindObjectOfType<CustomerSpawnManager>();
    }

    [HideInInspector] public enum OrderType
    {
        hamburger,
        coke
    }

    public void GiveOrder()
    {
        int orderCount = Random.Range(0, 2);

        for (int i = 0; i <= orderCount; i++)
        {
            GameObject order = Instantiate(orderPrefab, transform.position, Quaternion.identity, transform);
            order.transform.eulerAngles = new Vector3(0, 90, 0);
            order.GetComponent<Image>().sprite = orderSprite;
            customerWall.orders.Add(order.GetComponent<Order>());
        }
    }
    public void GivePreparedOrderToCustomer()
    {
        if (chef.HasHamburger())
        {
            for (int i = 0; i < customerWall.orders.Count; i++)
            {
                order = customerWall.orders[i];
                        
                if (order.hasBun == chef.currentHamburger.hasBun && order.hasCookedBurger == chef.currentHamburger.hasCookedBurger)
                {
                    Debug.Log("order is ready");

                    //Give prepared hamburger to customer
                    goldManager.EarnGold();
                    uiManager.PrintGold();
                    Destroy(customerWall.orders[0].gameObject);
                    customerWall.orders.Remove(customerWall.orders[0]);

                    Destroy(chef.currentHamburger.gameObject);
                    chef.currentHamburger = null;

                    //No more order to serve
                    if (customerWall.orders.Count == 0)
                    {
                        CustomerController customerController = customerSpawnManager.customerControllers[0];
                        customerController.UpdateStage(CustomerController.CustomerStage.going);
                    }

                    break;
                }
                else
                {
                    Debug.Log("order is not ready");
                    //Do not give hamburger, hamburger doesn't match with given order
                }
            }
        }
    }
}
