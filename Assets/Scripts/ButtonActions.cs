using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActions : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.Instance.CustomerPoints.ResetCustomers();
        GameManager.Instance.ChangeState(GameManager.GameState.StartGame);
    }

    public void StartGame()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.StartGame);
    }

    public void NextStage()
    {
        if (GameManager.Instance.CurrentStage == 1)
        {
            GameManager.Instance.CurrentStage = 2;
            GameManager.Instance.SetStage(2);
        }
        else if (GameManager.Instance.CurrentStage == 2)
        {
            GameManager.Instance.CurrentStage = 3;
            GameManager.Instance.SetStage(3);
        }
        else if (GameManager.Instance.CurrentStage == 3)
        {
            GameManager.Instance.CurrentStage = 1;
            GameManager.Instance.SetStage(1);
        }

        GameManager.Instance.CustomerPoints.ResetCustomers();
        GameManager.Instance.ChangeState(GameManager.GameState.StartGame);
    }
}
