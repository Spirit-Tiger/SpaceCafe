using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private Transform _orderCloud;
    public int CustomerId;
    public CustomerData Data;
    public Animator Anim;
    
    public enum CustomerState
    {
        Ordering,
        Exiting,
        InQueue,
    };
    CustomerState CurrentCustomerState;

    private void Awake()
    {
        _orderCloud = transform.GetChild(0);
        Anim = GetComponent<Animator>();

    }
    public void ChangeCustomerState(CustomerState state)
    {
        CurrentCustomerState = state;
        switch (state)
        {
            case CustomerState.Ordering:
                OrderingState();
                break;
            case CustomerState.Exiting:
                ExitingState();
                break;
        }
    }
    private void OrderingState()
    {
        _orderCloud.gameObject.SetActive(true);
    }

    private void ExitingState()
    {
     
    }


}
