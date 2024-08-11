using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerSpawnManager : MonoBehaviour
{
    public List<CustomerController> customerControllers;
    [SerializeField] GameObject customerPrefab;
    [SerializeField] Transform spawnPoint;
    int spawnedCustomersCount, totalCustomersCount;

    private void Start()
    {
        totalCustomersCount = Random.Range(3, 6);

        StartCoroutine(SpawnCustomerRoutine());
    }

    private void SpawnCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity, transform);
        customerControllers.Add(customer.GetComponent<CustomerController>());

        //Assign rotation
        customer.transform.eulerAngles = new Vector3(0, 180, 0);
    }
    private IEnumerator SpawnCustomerRoutine()
    {
        while (spawnedCustomersCount < totalCustomersCount)
        {
            // 5 ile 10 saniye arasýnda rastgele bir süre bekle
            float waitTime = Random.Range(5f, 10f);
            yield return new WaitForSeconds(waitTime);

            // Müþteri oluþtur
            SpawnCustomer();
        }
    }
}
