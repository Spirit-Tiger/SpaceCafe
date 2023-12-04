using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaMachine : MonoBehaviour
{
    public Transform SodaPoint;
    public bool IsCharged;
    public GameObject ChargeCup;

    public GameObject FinalSoda;
    public GameObject FinalSodaInstObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("SodaCup") && (GameManager.Instance.GravityState == 3 || GameManager.Instance.GravityState == 2))
        {
            other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.GetComponent<Rigidbody>().freezeRotation = true;
           other.transform.GetComponent<BoxCollider>().enabled = true;

            other.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            other.transform.position = SodaPoint.position;
            //other.transform.GetComponent<Item>().InMachine = true;
            other.transform.GetComponent<Item>().IsDragging = false;

            ChargeCup = other.gameObject;
            IsCharged = true;
        }
    }

    public void CreateFilledSoda()
    {
        FinalSodaInstObject = Instantiate(FinalSoda,SodaPoint.position,Quaternion.identity);
    }

}
