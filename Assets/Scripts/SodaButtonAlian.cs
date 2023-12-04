using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaButtonAlian : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponentInParent<SodaMachine>().ChargeCup != null)
                {

                    GiveSoda();
                }
            }
        }
    }
    public void GiveSoda()
    {
        if (GameManager.Instance.CurrentCustomer != null)
        {
            if (GameManager.Instance.CurrentCustomer.GetComponent<Customer>().Data.race == CustomerData.Race.Alian && transform.name == "cuboid_4")
            {
                Destroy(transform.GetComponentInParent<SodaMachine>().ChargeCup.gameObject);
                transform.GetComponentInParent<SodaMachine>().CreateFilledSoda();
            }
        }
    }

}
