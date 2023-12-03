using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameStage> Stages = new List<GameStage>(3);


    public CustomerPoints CustomerPoints;

    public GameObject EndingScreen;

    public Transform CurrentCustomer;
    public TextMeshProUGUI ScoreNumber;
    public TextMeshProUGUI EnergyNumber;

    [SerializeField]
    private float _energyCounter = 100;

    [SerializeField]
    private float _energyUsageSpeed = 2f;

    private float _defaultEnergy;
    private int _score = 0;

    public bool SwithGravitator = false;
    public bool QueueMoving = false;
    public bool OrderCustomerMoving = false;
    public bool MovingToExit = false;

    private bool _pause = false;

    public enum GameState
    {
        StartGame,
        Restart,
        GameOver,
    };

    public GameState State;

    public int CurrentSection;
    public int HighestSection;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _defaultEnergy = _energyCounter;

    }

    private void Start()
    {
        ChangeState(GameState.StartGame);
    }

    private void Update()
    {
        if (QueueMoving)
        {
            MoveQueue();
        }

        if (OrderCustomerMoving)
        {
            MoveToOrder();
        }

        if (MovingToExit)
        {
            NextCustomer();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            SwithGravitator = !SwithGravitator;

        }

        if (!SwithGravitator && !_pause)
        {
            _energyCounter -= _energyUsageSpeed * Time.deltaTime;
            _energyCounter = (float)Math.Round(_energyCounter, 2);
            EnergyNumber.text = _energyCounter.ToString();
        }

        if (_energyCounter <= 0)
        {
            ChangeState(GameState.GameOver);
        }

    }

    public void ChangeState(GameState state)
    {
        State = state;
        switch (state)
        {
            case GameState.StartGame:
                StartGame();
                break;
            case GameState.GameOver:
                GameOver();
                break;
        }
    }
    private void StartGame()
    {
        DOTween.KillAll();
        EndingScreen.SetActive(false);
        _pause = false;
        StartCoroutine(FirstCustomer());
        _energyCounter = _defaultEnergy;
    }

    private void GameOver()
    {
        EndingScreen.SetActive(true);
        _pause = true;
    }
    private IEnumerator FirstCustomer()
    {
        yield return new WaitForSeconds(1);
        CurrentCustomer = CustomerPoints.Customers.Dequeue();
        OrderCustomerMoving = true;
    }

    private void MoveToOrder()
    {
        CurrentCustomer.position = Vector3.MoveTowards(CurrentCustomer.position, CustomerPoints.OrderingPoint.position, 4f * Time.deltaTime);
        if (CurrentCustomer.position == CustomerPoints.OrderingPoint.position)
        {
            CurrentCustomer.GetComponent<Customer>().ChangeCustomerState(Customer.CustomerState.Ordering);
            QueueMoving = true;
            OrderCustomerMoving = false;
        }
    }

    public void GiveFood()
    {
        _score += 10;
        ScoreNumber.text = _score.ToString();
    }

    public void NextCustomer()
    {
        CurrentCustomer.position = Vector3.MoveTowards(CurrentCustomer.position, CustomerPoints.ExitPoint.position, 4f * Time.deltaTime);
        if (CurrentCustomer.position == CustomerPoints.ExitPoint.position)
        {
            CurrentCustomer.GetComponent<Customer>().ChangeCustomerState(Customer.CustomerState.Exiting);
            GiveFood();
            CurrentCustomer = CustomerPoints.Customers.Dequeue();
            OrderCustomerMoving = true;
            MovingToExit = false;
        }
           
           
    }

    public Transform LastUser = null;
    public void MoveQueue()
    {
        int i = 0;
       
        foreach (Transform cust in CustomerPoints.Customers)
        {
            LastUser = cust;
            cust.position = Vector3.MoveTowards(cust.position, CustomerPoints.Points[i].position, 5 * Time.deltaTime);
            i++;
        }
        if (LastUser.position == CustomerPoints.Points[i - 1].position)
        {
            QueueMoving = false;
        }
    }
}
