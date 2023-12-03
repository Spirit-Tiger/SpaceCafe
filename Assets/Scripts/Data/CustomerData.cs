using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewCust",menuName ="Customers")]
public class CustomerData : ScriptableObject
{
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
       Burger,
       Soup,
       Soda
    };

    public Food food;
}
