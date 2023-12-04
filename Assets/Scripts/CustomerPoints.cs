using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPoints : MonoBehaviour
{
    public Transform OrderingPoint;
    public Transform ExitPoint;
    public List<Transform> Points;
    public Queue<Transform> Customers = new Queue<Transform>();
    public List<Transform> InGameCustomers = new List<Transform>();

    private void Start()
    {

        for (int i = 0; i < GameManager.Instance.PresentStage.Customers.Count; i++)
        {

            GameObject customer = Instantiate(GameManager.Instance.PresentStage.Customers[i].custPrefub);
            InGameCustomers.Add(customer.transform);
            customer.GetComponent<Customer>().CustomerId = i;
            customer.GetComponent<Customer>().Data = GameManager.Instance.PresentStage.Customers[i];
            customer.transform.position = Points[i].position;
            Customers.Enqueue(customer.transform);

        }



    }

    public void ResetCustomers()
    {
        foreach (var c in InGameCustomers)
        {
            Destroy(c.gameObject);
        }

        InGameCustomers.Clear();
        Customers.Clear();
        for (int i = 0; i < GameManager.Instance.PresentStage.Customers.Count; i++)
        {

            GameObject customer = Instantiate(GameManager.Instance.PresentStage.Customers[i].custPrefub);
            InGameCustomers.Add(customer.transform);
            customer.GetComponent<Customer>().CustomerId = i;
            customer.GetComponent<Customer>().Data = GameManager.Instance.PresentStage.Customers[i];
            customer.transform.position = Points[i].position;
            if (customer.GetComponent<Customer>().Data.race == CustomerData.Race.Orb)
            {
                customer.transform.position = customer.transform.position + new Vector3(0, 2.5f, 0);
            }
            Customers.Enqueue(customer.transform);

        }
    }
}
