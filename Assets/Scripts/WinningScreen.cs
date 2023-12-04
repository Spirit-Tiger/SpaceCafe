using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinningScreen : MonoBehaviour
{
    public TextMeshProUGUI NextStageButton;

    private void OnEnable()
    {
        if (GameManager.Instance.CurrentStage == 3)
        {
            NextStageButton.text = "Start from the start";
        }
    }
}
