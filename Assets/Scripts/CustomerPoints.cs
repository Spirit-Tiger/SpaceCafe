using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPoints : MonoBehaviour
{
    public Transform OrderingPoint;
    public Transform ExitPoint;
    public List<Transform> Points;
    public List<Transform> Customers;
    private void Awake()
    {
        for (int i = 0; i < Customers.Count; i++)
        {
            Customers[i].position = Points[i].position;
        }
    }
}
