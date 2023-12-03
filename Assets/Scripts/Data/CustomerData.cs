using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewCust",menuName ="Customers")]
public class CustomerData : ScriptableObject
{
   public GameObject custPrefub;
   public enum Race
    {
        Human,
        Alian,
        Gorila,
        Shroomie,
        Orb
    };
    public Race race;
    public enum Food
    {
       Burger = 50,
       Soup = 40,
       Soda = 35
    };

    public Food food;
}
