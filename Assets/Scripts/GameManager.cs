using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    [SerializeField] private STATE currentState;
    [SerializeField] private int Coin;

    private void OnEnable()
    {
        Actions.OnStateChange += OnStateChange;
        Actions.OnGetCoin += OnGetCoin;
    }


    private void OnDisable()
    {
        Actions.OnStateChange -= OnStateChange;
        Actions.OnGetCoin -= OnGetCoin;
    }
    private void OnGetCoin(int obj)
    {
        Coin += obj;
        Actions.TotalCoin.Invoke(Coin);
    }

    private void OnStateChange(STATE obj)
    {
        currentState = obj;
        switch (currentState)
        {
            case STATE.PLAY:
                Time.timeScale = 1;
                break;
            case STATE.RESUME:
                Time.timeScale = 1;
                break;
            case STATE.PAUSE:
                Time.timeScale = 0;
                break;
            case STATE.GAMEOVER:
                Time.timeScale = 0;
                break;
            case STATE.WIN:
                Time.timeScale = 0;
                break;
            default:
                break;
        }
    }
}
