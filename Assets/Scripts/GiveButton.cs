using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveButton : MonoBehaviour
{
    public Transform CurrentDish;
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && CurrentDish.GetComponent<CurrentDish>().Complete)
        {
            if (GameManager.Instance.CustomerPoints.Customers.Count > 0)
            {
                if (CurrentDish.GetComponent<CurrentDish>().Correct)
                {
                    GameManager.Instance.Score += (int)GameManager.Instance.CurrentCustomer.GetComponent<Customer>().Data.food;
                }
                else
                {
                    GameManager.Instance.Score += (int)GameManager.Instance.CurrentCustomer.GetComponent<Customer>().Data.food/2;
                }

                GameManager.Instance.MovingToExit = true;
                GameManager.Instance.CurrentCustomer.GetChild(0).gameObject.SetActive(false);
                GameManager.Instance.CurrentCustomer.GetChild(1).gameObject.SetActive(true);
            }
            CurrentDish.GetComponent<CurrentDish>().Reset();
        }
    }
}
