using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{

    public static Cooking Instance;

    public GameObject TopPart;
    public GameObject BottomPart;
    public GameObject Kotleta;
    public GameObject Cheese;
    public GameObject Salat;

    public List<Transform> FoodPoints = new List<Transform>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }   

    public void GiveIngredients(CustomerData.Food food){
    if((int)food == 50)

        {
            List<GameObject> list = new List<GameObject>();
            list.Add(BottomPart);
            list.Add(Cheese);
            list.Add(Salat);
            list.Add(TopPart);
            list.Add(Kotleta);
            for (int i =0; i < 5; i++)
            {
                GameObject ingerdient = Instantiate(list[i]);
                ingerdient.transform.position = FoodPoints[i].position;
            }
        }
    }
}
