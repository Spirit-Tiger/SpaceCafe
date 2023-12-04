using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameStage> Stages = new List<GameStage>(3);

    public CustomerPoints CustomerPoints;

    public GameObject EndingScreen;
    public GameObject WinningScreen;

    public Transform LastUser;
    public Transform CurrentCustomer;

    public TextMeshProUGUI ScoreNumber;
    public TextMeshProUGUI EnergyNumber;

    public TextMeshProUGUI GlobalScoreText;
    public TextMeshProUGUI TotalStageScore;
    public TextMeshProUGUI ScoreForEnergy;
    public TextMeshProUGUI ScoreForTime;
    public TextMeshProUGUI ScoreWinningScreen;
    public TextMeshProUGUI ScoreEndingScreen;
    public TextMeshProUGUI StageNumberScreen;

    public TextMeshProUGUI StageClearedText;
    public TextMeshProUGUI NextBtnText;

    public GameStage Stage1;
    public GameStage Stage2;
    public GameStage Stage3;

    public float GlobalScore;

    public float Stage1Score;
    public float Stage2Score;
    public float Stage3Score;

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
        Complete,
        GameOver,
    };

    public GameState State;

    public int CurrentStage = 1;
    public int LastStage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        CurrentStage = 1;

        SetStage(CurrentStage);
    }

    public void SetStage(int stage)
    {
        if (stage == 1)
        {
            CurrentStage = 1;
            LastStage = 1;
            PresentStage = Stage1;
        }
        if (stage == 2)
        {
            CurrentStage = 2;
            LastStage = 2;
            PresentStage = Stage2;

        }
        if (stage == 3)
        {
            CurrentStage = 3;
            LastStage = 3;
            PresentStage = Stage3;
        }
        StageNumberScreen.text = CurrentStage.ToString();
        _energyCounter = PresentStage.Energy;
        EnergyNumber.text = _energyCounter.ToString();
        transform.GetComponent<Timer>().SetTime();
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
            case GameState.Complete:
                StartCoroutine(StageCleared());
                break;
            case GameState.GameOver:
                GameOver();
                break;
        }
    }
    private void StartGame()
    {
        transform.GetComponent<Timer>().SetTime();
        Score = 0;
        ScoreNumber.text = Score.ToString();

        if (CurrentStage == 1)
        {
            GlobalScore = 0;
            Stage1Score = 0;
        }
        if (CurrentStage == 2)
        {
            GlobalScore -= Stage1Score;
            Stage2Score = 0;

        }
        if (CurrentStage == 3)
        {
            GlobalScore -= Stage2Score;
            Stage3Score = 0;
        }
        WinningScreen.SetActive(false);
        EndingScreen.SetActive(false);
        Cooking.Instance.DestroyIngredients();
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
        ScoreEndingScreen.text = Score.ToString();
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
                ChangeState(GameState.Complete);
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

        WinningScreen.SetActive(true);
        float scoreForTime = (float)Math.Round(transform.GetComponent<Timer>().GetTimeLeft() * 0.2f, 2);
        float scoreForEnergy = (float)Math.Round(_energyCounter * 1.2f, 2); ;

        float totalScore = Score + scoreForTime + scoreForEnergy;
        if (CurrentStage == 1)
        {
            Stage1Score = totalScore;
            GlobalScore = totalScore;

            StageClearedText.text = "Stage 1 Complete";
            NextBtnText.text = "Next Stage";
        }
        if (CurrentStage == 2)
        {
            Stage2Score = totalScore;
            GlobalScore = Stage1Score + totalScore;

            StageClearedText.text = "Stage 2 Complete";
            NextBtnText.text = "Next Stage";
        }
        if (CurrentStage == 3)
        {
            Stage3Score = totalScore;
            GlobalScore = Stage1Score + Stage2Score + totalScore;

            StageClearedText.text = "Game Complete Stage 3 Complete";
            NextBtnText.text = "Start from beginning";
        }
        GlobalScoreText.text = GlobalScore.ToString();

        ScoreForTime.text = scoreForTime.ToString();
        ScoreForEnergy.text = scoreForEnergy.ToString();
        TotalStageScore.text = totalScore.ToString();
        ScoreWinningScreen.text = Score.ToString();
    }

}
