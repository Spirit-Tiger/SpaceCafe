using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CustomerPoints CustomerPoints;
    private Tween _tween;

    public Transform CurrentCustomer;
    public TextMeshProUGUI ScoreNumber;
    private int _score = 0;

    public bool SwithGravitator = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (CustomerPoints.Customers.Count > 0)
            {
                NextCustomer();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }
    }

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
  
    private void GameOver()
    {

    }
    private IEnumerator FirstCustomer()
    {
        yield return new WaitForSeconds(1);
        CurrentCustomer = CustomerPoints.Customers.Dequeue();
        _tween = CurrentCustomer.DOMove(CustomerPoints.OrderingPoint.position, 1).OnComplete(() => CurrentCustomer.GetComponent<Customer>().ChangeCustomerState(Customer.CustomerState.Ordering));
    }

    public void GiveFood()
    {
        CurrentCustomer.GetChild(0).gameObject.SetActive(false);
        _score += 10;
        ScoreNumber.text = _score.ToString();
    }

    public void NextCustomer()
    {
        CurrentCustomer.DOMove(CustomerPoints.ExitPoint.position, 1).OnComplete(() => CurrentCustomer.GetComponent<Customer>().ChangeCustomerState(Customer.CustomerState.Exiting));
        GiveFood();
        CurrentCustomer = CustomerPoints.Customers.Dequeue();
        _tween = CurrentCustomer.DOMove(CustomerPoints.OrderingPoint.position, 1).OnComplete(() => CurrentCustomer.GetComponent<Customer>().ChangeCustomerState(Customer.CustomerState.Ordering));

    }

    public void MoveQueue()
    {
        int i = 0;
        foreach (Transform cust in CustomerPoints.Customers)
        {
            cust.DOMove(CustomerPoints.Points[i].position, 1);
            i++;
        }
    }
}
