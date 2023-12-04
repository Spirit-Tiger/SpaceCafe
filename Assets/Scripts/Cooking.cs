using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using static CustomerData;

public class Cooking : MonoBehaviour
{

    public static Cooking Instance;

    public GameObject Burger;
    public GameObject Soda;
    public GameObject Soup;

    public GameObject TopPart;
    public GameObject BottomPart;
    public GameObject Kotleta;
    public GameObject Cheese;
    public GameObject Salat;

    public GameObject Shroom;
    public GameObject Green;
    public GameObject Jija;
    public GameObject Powder;

    public GameObject SodaPart;

    public CustomerData.Food Food;


    public bool CanSell = false;

    public List<Transform> FoodPoints = new List<Transform>();
    public List<Transform> FoodPoints2 = new List<Transform>();
    public List<GameObject> FoodInGame = new List<GameObject>();
    public List<GameObject> FoodInGameSoda = new List<GameObject>();
    public List<GameObject> FoodInGameSoup = new List<GameObject>();
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
            if ((int)Food == 50)
            {
                for (int i = 0; i < FoodInGame.Count; i++)
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
                                foreach (var go in FoodInGame)
                                {
                                    go.GetComponent<BoxCollider>().enabled = true;
                                }
                                count = 0;
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
                                count = 0;
                            }
                        }
                    }
                }
            }
            if ((int)Food == 40)
            {
                for (int i = 0; i < FoodInGameSoup.Count; i++)
                {
                    if (GameManager.Instance.CurrentCustomer.GetComponent<Customer>().CustomerId % 2 == 0)
                    {
                        FoodInGameSoup[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                        FoodInGameSoup[i].transform.position = Vector3.MoveTowards(FoodInGameSoup[i].transform.position, FoodPoints[i].position, 10f * Time.deltaTime);
                        if (FoodInGameSoup[i].transform.position == FoodPoints[i].position)
                        {
                            count++;
                            if (i == 3 && count == 3)
                            {
                                _givingFood = false;
                                foreach (var go in FoodInGameSoup)
                                {
                                    go.GetComponent<BoxCollider>().enabled = true;
                                }
                                count = 0;
                            }
                        }
                    }
                    else
                    {
                        FoodInGameSoup[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                        FoodInGameSoup[i].transform.position = Vector3.MoveTowards(FoodInGameSoup[i].transform.position, FoodPoints2[i].position, 10f * Time.deltaTime);
                        if (FoodInGameSoup[i].transform.position == FoodPoints2[i].position)
                        {
                            count++;
                            if (i == 3 && count == 3)
                            {
                                _givingFood = false;
                                foreach (var go in FoodInGameSoup)
                                {
                                    go.GetComponent<BoxCollider>().enabled = true;
                                }
                                count = 0;
                            }
                        }
                    }
                }
            }
            if ((int)Food == 30)
            {
                for (int i = 0; i < FoodInGameSoda.Count; i++)
                {
                    if (GameManager.Instance.CurrentCustomer.GetComponent<Customer>().CustomerId % 2 == 0)
                    {
                        FoodInGameSoda[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                        FoodInGameSoda[i].transform.position = Vector3.MoveTowards(FoodInGameSoda[i].transform.position, FoodPoints[i].position, 10f * Time.deltaTime);
                        if (FoodInGameSoda[i].transform.position == FoodPoints[i].position)
                        {
                            if (i == FoodInGameSoda.Count - 1)
                            {
                                _givingFood = false;
                                foreach (var go in FoodInGameSoda)
                                {
                                    go.GetComponent<BoxCollider>().enabled = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        FoodInGameSoda[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                        FoodInGameSoda[i].transform.position = Vector3.MoveTowards(FoodInGameSoda[i].transform.position, FoodPoints2[i].position, 10f * Time.deltaTime);
                        if (FoodInGameSoda[i].transform.position == FoodPoints2[i].position)
                        {
                            if (i == FoodInGameSoda.Count - 1)
                            {
                                _givingFood = false;
                                foreach (var go in FoodInGameSoda)
                                {
                                    go.GetComponent<BoxCollider>().enabled = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public void GiveIngredients(CustomerData.Food food)
    {
        Debug.Log("Give123");
        List<GameObject> list = new List<GameObject>();
        List<GameObject> list2 = new List<GameObject>();
        Food = food;
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

        if ((int)food == 40)
        {
            list.Add(Shroom);
            list.Add(Green);
            list.Add(Jija);
            list.Add(Powder);
            Debug.Log("List " + list[0].name);
            for (int i = 0; i < 4; i++)
            {

                GameObject ingerdient = Instantiate(list[i], StartPosition.position, Quaternion.identity);
                ingerdient.GetComponent<BoxCollider>().enabled = false;
                list2.Add(ingerdient);
            }
            FoodInGameSoup = list2;
        }

        if ((int)food == 30)
        {
            list.Add(SodaPart);

            Debug.Log("List " + list[0].name);
            for (int i = 0; i < 1; i++)
            {

                GameObject ingerdient = Instantiate(list[i], StartPosition.position, Quaternion.identity);
                //ingerdient.GetComponent<BoxCollider>().enabled = false;
                list2.Add(ingerdient);
            }
            FoodInGameSoda = list2;
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
        if (FoodInGameSoda.Count > 0)
        {
            foreach (GameObject go in FoodInGame)
            {
                Destroy(go);
            }
        }
        if (FoodInGameSoup.Count > 0)
        {
            foreach (GameObject go in FoodInGame)
            {
                Destroy(go);
            }
        }
    }
}
