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

    public Transform LastUser;
    public Transform CurrentCustomer;

    public TextMeshProUGUI ScoreNumber;
    public TextMeshProUGUI EnergyNumber;

    public TextMeshProUGUI TotalScore;
    public TextMeshProUGUI ScoreForEnergy;
    public TextMeshProUGUI ScoreForTime;

    public GameStage Stage1;
    public GameStage Stage2;
    public GameStage Stage3;

    public PlayerControls PlayerControls;

    public GameStage PresentStage;

    [SerializeField]
    private float _energyCounter = 100;

    [SerializeField]
    private float _energyUsageSpeed = 2f;

    public int Score = 0;

    public int GravityState = 1;
    public Animator LeverAnimator;
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

    public int CurrentStage = 1;
    public int HighestStage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        CurrentStage = 1;

        if (CurrentStage == 1)
        {
            PresentStage = Stage1;
        }
        if (CurrentStage == 2)
        {
            PresentStage = Stage2;

        }
        if (CurrentStage == 3)
        {
            PresentStage = Stage3;
        }
        _energyCounter = PresentStage.Energy;
        EnergyNumber.text = _energyCounter.ToString();


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

        if (GravityState == 3 && !_pause)
        {
            _energyCounter -= _energyUsageSpeed * Time.deltaTime;
            _energyCounter = (float)Math.Round(_energyCounter, 2);
            EnergyNumber.text = _energyCounter.ToString();
        }


        if (GravityState == 1 && !_pause)
        {
            if (_energyCounter < 100)
            {
                _energyCounter += 0.7f * Time.deltaTime;
                _energyCounter = (float)Math.Round(_energyCounter, 2);
                EnergyNumber.text = _energyCounter.ToString();
            }
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
        EndingScreen.SetActive(false);
        Cooking.Instance.DestroyIngredients();
        transform.GetComponent<Timer>().SetTime();
        PlayerControls.SwitchToStart();
        LeverAnimator.SetTrigger("L_1");
        GravityState = 1;
        _pause = false;
        _energyCounter = PresentStage.Energy;

        QueueMoving = false;
        MovingToExit = false;
        StartCoroutine(FirstCustomer());
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
            Cooking.Instance.GiveIngredients(CurrentCustomer.GetComponent<Customer>().Data.food);
            CurrentCustomer.GetComponent<Customer>().ChangeCustomerState(Customer.CustomerState.Ordering);
            if (CustomerPoints.Customers != null)
            {
                QueueMoving = true;
            }
            OrderCustomerMoving = false;
        }
    }

    public void GiveFood()
    {
        ScoreNumber.text = Score.ToString();
    }

    public void NextCustomer()
    {
        CurrentCustomer.position = Vector3.MoveTowards(CurrentCustomer.position, CustomerPoints.ExitPoint.position, 4f * Time.deltaTime);
        if (CurrentCustomer.position == CustomerPoints.ExitPoint.position)
        {
            CurrentCustomer.gameObject.SetActive(false);
            CurrentCustomer.GetComponent<Customer>().ChangeCustomerState(Customer.CustomerState.Exiting);
            GiveFood();
            if (CustomerPoints.Customers.Count > 0)
            {
                OrderCustomerMoving = true;
                MovingToExit = false;
                CurrentCustomer.GetComponent<Customer>().ChangeCustomerState(Customer.CustomerState.Exiting);
                CurrentCustomer = CustomerPoints.Customers.Dequeue();
            }
            else
            {
                MovingToExit = false;
                StartCoroutine(StageCleared());
            }

        }
    }
    public void MoveQueue()
    {
        int i = 0;

        if (CustomerPoints.Customers.Count > 0)
        {
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
        else
        {
            QueueMoving = false;
        }
    }
    private IEnumerator StageCleared()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Stage Completed");
        CalculateStageScore();
    }

    private void CalculateStageScore()
    {
        _pause = true; 

        float scoreForTime = transform.GetComponent<Timer>().GetTimeLeft() * 0.2f;
        float scoreForEnergy = _energyCounter * 1.2f;

        float totalScore = Score + scoreForTime + scoreForEnergy;

        ScoreForTime.text = scoreForTime.ToString();
        ScoreForEnergy.text = scoreForEnergy.ToString();
        TotalScore.text = totalScore.ToString();
    }

}
