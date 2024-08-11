using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [HideInInspector] public CustomerStage stage;
    CustomerMover customerMover;
    CustomerOrderManager customerOrderManager;

    [HideInInspector]
    public enum CustomerStage
    {
        coming,
        waiting,
        going
    }

    private void Start()
    {
        stage = CustomerStage.coming;
        customerMover = GetComponent<CustomerMover>();
        customerOrderManager = FindObjectOfType<CustomerOrderManager>();
    }

    private void Update()
    {
        GiveOrder();
        ManageStates();
    }

    private void ManageStates()
    {
        switch (stage)
        {
            case CustomerStage.coming:
                customerMover.MoveToStandPoint();
                break;
            case CustomerStage.waiting:
                GiveOrder();
                break;
            case CustomerStage.going:
                customerMover.MoveToLeavingPoint();
                break;
        }
    }

    public void UpdateStage(CustomerStage newStage)
    {
        stage = newStage;
    }

    private void GiveOrder()
    {
        //Customer arrived stand point, will stop and give order
        if (customerMover.HasReachedStandPoint() && stage == CustomerStage.coming)
        {
            stage = CustomerStage.waiting;
            customerOrderManager.GiveOrder();
        }
    }
}
