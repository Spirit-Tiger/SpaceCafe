using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPoints : MonoBehaviour
{
    public Transform OrderingPoint;
    public Transform ExitPoint;
    public List<Transform> Points;
    public Queue<Transform> Customers = new Queue<Transform>();

    public GameObject CustomerPrefub;
    private void Awake()
    {
       
        for (int i = 0; i < Points.Count; i++)
        {
            GameObject customer = Instantiate(CustomerPrefub);
            customer.GetComponent<Customer>().CustomerId = i;
            customer.transform.position = Points[i].position;
            Customers.Enqueue(customer.transform);
            
        }
    }
}
