using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStage", menuName = "Stage")]
public class GameStage : ScriptableObject
{
    public int stageNumber;

    public List<GameObject> Customers;

    public int Time;
}
