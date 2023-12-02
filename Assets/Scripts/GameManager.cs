using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CustomerPoints CustomerPoints;
    private Tween _tween;

    public enum GameState
    {
        StartGame,
        Restart,
        GameOver,
    };
    GameState State;
    public int CurrentSection;
    public int HighestSection;
    private void Start()
    {
        ChangeState(GameState.StartGame);
    }
    public void ChangeState(GameState state)
    {
        State = state;
        switch (state)
        {
            case GameState.StartGame:
                StartGame();
                break;
            case GameState.Restart:
                break;
            case GameState.GameOver:
                break;
        }
    }
    private void StartGame()
    {
        StartCoroutine(FirstCustomer());
    }
    private void Restart()
    {
    }
    private void GameOver()
    {
    }
    private IEnumerator FirstCustomer()
    {
        yield return new WaitForSeconds(1);
        _tween = CustomerPoints.Customers[0].DOMove(CustomerPoints.OrderingPoint.position, 1);
        CustomerPoints.Customers[0].GetComponent<Customer>().ChangeCustomerState(Customer.CustomerState.Ordering);
    }
}
