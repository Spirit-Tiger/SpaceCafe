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
}
