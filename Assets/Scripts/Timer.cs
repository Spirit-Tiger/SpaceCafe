using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;
    public TextMeshProUGUI timerText;
    private float _timeLeft = 0f;
    private bool _timerOn = false;
    [SerializeField] private float _minusTime = 0.08f;
    private void Start()
    {
        _timeLeft = time;
        _timerOn = true;
    }
    private void Update()
    {
        if (_timerOn)
        {
            if (_timeLeft > 0)
            {
                if (GameManager.Instance.GravityState == 2)
                {
                    _timeLeft -= Time.deltaTime + _minusTime;
                    UpdateTimeText();
                }
                else
                {
                    _timeLeft -= Time.deltaTime;
                    UpdateTimeText();
                }
            }
            else
            {
                _timeLeft = time;
                _timerOn = false;
            }

            if (GameManager.Instance.State == GameManager.GameState.GameOver)
            {
                _timerOn = false;
            }
        }

        if (_timeLeft <= 0 && GameManager.Instance.State != GameManager.GameState.GameOver)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.GameOver);
        }
    }
    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;
        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
