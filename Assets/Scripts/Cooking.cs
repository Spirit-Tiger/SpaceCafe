using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using static CustomerData;

public class Cooking : MonoBehaviour
{

    public static Cooking Instance;

    public GameObject TopPart;
    public GameObject BottomPart;
    public GameObject Kotleta;
    public GameObject Cheese;
    public GameObject Salat;

    public bool CanSell = false;

    public List<Transform> FoodPoints = new List<Transform>();
    public List<Transform> FoodPoints2 = new List<Transform>();
    public List<GameObject> FoodInGame = new List<GameObject>();
    public Transform StartPosition;

    private bool _givingFood = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (_givingFood)
        {
            int count = 0;
            for(int i = 0; i < FoodInGame.Count; i++)
            {
                if (GameManager.Instance.CurrentCustomer.GetComponent<Customer>().CustomerId % 2 == 0)
                {
                    FoodInGame[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    FoodInGame[i].transform.position = Vector3.MoveTowards(FoodInGame[i].transform.position, FoodPoints[i].position, 10f * Time.deltaTime);
                    if (FoodInGame[i].transform.position == FoodPoints[i].position)
                    {
                        count++;
                        if (i == 4 && count == 4)
                        {
                            _givingFood = false;
                            foreach(var go in FoodInGame)
                            {
                                go.GetComponent<BoxCollider>().enabled = true;
                            }
                        }
                    }
                }
                else
                {
                    FoodInGame[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    FoodInGame[i].transform.position = Vector3.MoveTowards(FoodInGame[i].transform.position, FoodPoints2[i].position, 10f * Time.deltaTime);
                    if (FoodInGame[i].transform.position == FoodPoints2[i].position)
                    {
                        count++;
                        if (i == 4 && count == 4)
                        {
                            _givingFood = false;
                            foreach (var go in FoodInGame)
                            {
                                go.GetComponent<BoxCollider>().enabled = true;
                            }
                        }
                    }
                }

            }
        }
    }
    public void GiveIngredients(CustomerData.Food food)
    {
        List<GameObject> list = new List<GameObject>();
        List<GameObject> list2 = new List<GameObject>();
        if ((int)food == 50)
        {    
            list.Add(BottomPart);
            list.Add(Kotleta);
            list.Add(Cheese);
            list.Add(Salat);
            list.Add(TopPart);
            Debug.Log("List " + list[0].name);
            for (int i = 0; i < 5; i++)
            {

                GameObject ingerdient = Instantiate(list[i], StartPosition.position, Quaternion.identity);
                ingerdient.GetComponent<BoxCollider>().enabled = false;
                list2.Add(ingerdient);
            }
            FoodInGame = list2;
        }

        _givingFood = true;


    }

    public void DestroyIngredients()
    {
        if (FoodInGame.Count > 0)
        {
            foreach (GameObject go in FoodInGame)
            {
                Destroy(go);
            }
        }
    }
}
