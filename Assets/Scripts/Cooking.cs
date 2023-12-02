using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{

    public static Cooking Instance;

    public Transform Burger;
    public Transform TopPart;
    public Transform BottomPArt;
    public Transform Kotleta;
    public Transform Cheese;
    public Transform Salat;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }   
}
