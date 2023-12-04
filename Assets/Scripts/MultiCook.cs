using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCook : MonoBehaviour
{
    public GameObject CookedFood;
    public Transform SpawnPoint;
    public bool Cooking = false;
    public bool TopIsOpened = false;
    public GameObject FoodOut;


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !Cooking)
        {
            transform.GetChild(1).transform.rotation = Quaternion.Euler(0f,0f,90f);
            TopIsOpened = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if( collision.gameObject.tag == "mFood" && !Cooking && TopIsOpened)
        {
            Cooking = true;
            collision.gameObject.SetActive(false);
            transform.GetChild(1).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            StartCoroutine(Open());
            TopIsOpened = false;
        }
    }

    private IEnumerator Open()
    {
        yield return new WaitForSeconds(2f);
        Cooking = false;
        TopIsOpened = true;
        transform.GetChild(1).transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        FoodOut = Instantiate(CookedFood, SpawnPoint.position, Quaternion.identity);
    }


}
