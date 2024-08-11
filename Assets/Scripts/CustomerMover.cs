using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMover : MonoBehaviour
{
    Transform customerStandPoint, leavePoint;
    float comingSpeed = 10f, goingSpeed = 7.5f;
    int distanceBetweenCars = 6;
    CustomerSpawnManager customerSpawnManager;
    
    private void Start()
    {
        customerSpawnManager = FindObjectOfType<CustomerSpawnManager>();
        customerStandPoint = GameObject.Find("Customer Stand Point").transform;
        leavePoint = GameObject.Find("Customer Leave Point").transform;
    }

    public void MoveToStandPoint()
    {
        int index = customerSpawnManager.customerControllers.IndexOf(GetComponent<CustomerController>());
        Vector3 targetPosition = new Vector3
            (
            customerStandPoint.position.x, 
            customerStandPoint.position.y,
            customerStandPoint.position.z + distanceBetweenCars * index
            );

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, comingSpeed * Time.deltaTime);
    }

    public void MoveToLeavingPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, leavePoint.position, goingSpeed * Time.deltaTime);
    }

    public bool HasReachedStandPoint()
    {
        if (transform.position.z <= customerStandPoint.position.z)
        {

            return true;
        }
        return false;
    }
}
