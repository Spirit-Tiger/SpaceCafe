using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private GameObject _orderCloud;
    public enum CustomerState
    {
        Ordering,
        Exiting,
        InQueue,
    };
    CustomerState CurrentCustomerState = CustomerState.InQueue;
    public void ChangeCustomerState(CustomerState state)
    {
        CurrentCustomerState = state;
        switch (state)
        {
            case CustomerState.Ordering:
                OrderingState();
                break;
            case CustomerState.Exiting:
                break;
            case CustomerState.InQueue:
                break;
        }
    }
    private void OrderingState()
    {
        _orderCloud.SetActive(true);
    }
}
