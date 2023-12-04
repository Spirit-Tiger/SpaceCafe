using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class SodaButton : MonoBehaviour
{
    public CurrentDish CurrentDish;
    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 7))
        {
            if (Input.GetMouseButtonDown(0) && transform.GetComponentInParent<SodaMachine>().IsCharged == true)
            {
                GiveSoda();
            }
        }
    }
    public void GiveSoda()
    {
        if (GameManager.Instance.CurrentCustomer != null && transform.GetComponentInParent<SodaMachine>().FinalSodaInstObject == null)
        {
            Destroy(transform.GetComponentInParent<SodaMachine>().ChargeCup.gameObject);
            transform.GetComponentInParent<SodaMachine>().CreateFilledSoda();
            CurrentDish.Check();
        }
    }
}
