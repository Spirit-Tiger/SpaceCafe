using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public Transform GivePoint;
    public Transform CurrentDish;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ingredient" && CurrentDish.GetComponent<CurrentDish>().Complete)
        {
            other.transform.position = GivePoint.position;
            other.transform.rotation = Quaternion.Euler(0, 0, 0);   
            other.transform.GetComponent<Rigidbody>().freezeRotation = true;
            other.transform.SetParent(CurrentDish);
        }
    }
}
