using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCook : MonoBehaviour
{
    public static MultiCook Instance;
    public GameObject CookedFood;
    public Transform SpawnPoint;
    public bool Cooking = false;
    public bool TopIsOpened = false;
    public GameObject FoodOut;
    public int CookCounter = 0;
    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {

            Instance = this;
        }


    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !Cooking)
        {
            animator.SetTrigger("Open"); //transform.GetChild(1).transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            CookCounter = 0;
            TopIsOpened = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SoupIng" && TopIsOpened)
        {
            Cooking = true;
            collision.gameObject.SetActive(false);

            CookCounter++;
            if (CookCounter == 2)
            {
                animator.SetTrigger("Close"); //transform.GetChild(1).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                TopIsOpened = false;
                StartCoroutine(Open());
            }
        }
    }

    private IEnumerator Open()
    {
        yield return new WaitForSeconds(2f);
        Cooking = false;
        TopIsOpened = true;
        animator.SetTrigger("Open"); //transform.GetChild(1).transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        FoodOut = Instantiate(CookedFood, SpawnPoint.position, Quaternion.identity);
        CookCounter = 0;
        animator.SetTrigger("Close");
    }


}
