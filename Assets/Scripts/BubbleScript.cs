using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    private void OnEnable()
    {
        CustomerData.Food cusFood = GameManager.Instance.CurrentCustomer.GetComponent<Customer>().Data.food;
        
        if((int)cusFood == 50)
        {   
            GameObject go = Instantiate(Cooking.Instance.Burger, transform.position + new Vector3(0,0.2f,0.2f),Quaternion.Euler(0f, - 25f, 0f));
            go.transform.SetParent(transform);
        }
        if ((int)cusFood == 40)
        {
            GameObject go = Instantiate(Cooking.Instance.Soup);
            go.transform.SetParent(transform);
        }
        if ((int)cusFood == 30)
        {
            GameObject go = Instantiate(Cooking.Instance.Soda);
            go.transform.SetParent(transform);
        }
    }
}
