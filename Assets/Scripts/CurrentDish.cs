using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentDish : MonoBehaviour
{
    public bool Complete = false;
    public int Counter = 0;
    public bool Correct = true;
    public int PrevPosition = 0;
    public void Check()
    {
        if (GameManager.Instance.CurrentCustomer.GetComponent<Customer>().Data.food == CustomerData.Food.Burger && Counter == 4)
        {
            Complete = true;
            Debug.Log("Complete");
        }
        else if (GameManager.Instance.CurrentCustomer.GetComponent<Customer>().Data.food == CustomerData.Food.Soda && Counter == 3)
        {
            Complete = true;
            Debug.Log("Complete");
        }
        else if (GameManager.Instance.CurrentCustomer.GetComponent<Customer>().Data.food == CustomerData.Food.Soup && Counter == 3)
        {
            Complete = true;
            Debug.Log("Complete");
        }
    }

    public void Reset()
    {
        Complete = false;
        Counter = 0;
        Correct = true;
        PrevPosition = 0;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
