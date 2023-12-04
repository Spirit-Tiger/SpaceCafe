using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmitName : MonoBehaviour
{
    public void SubmitRecord()
    {
        Leaderboard.Instance.SetLeaderts(Leaderboard.Instance.NameInput.text,GameManager.Instance.GlobalScore);
    }
}
