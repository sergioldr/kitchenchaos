using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnGameStateChanged;
    public static GameManager Instance { get; private set; }
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        Playing,
        GameOver,
        Paused
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayTimer;
    private float gamePlayTimerMax = 10f;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;

                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;

                if (countdownToStartTimer < 0f)
                {
                    state = State.Playing;
                    gamePlayTimer = gamePlayTimerMax;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                gamePlayTimer -= Time.deltaTime;

                if (gamePlayTimer < 0f)
                {
                    state = State.GameOver;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.Playing;
    }

    public bool IsGameWaitingToStart()
    {
        return state == State.WaitingToStart;
    }

    public bool IsGameCountdownToStart()
    {
        return state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayTimer()
    {
        return 1 - (gamePlayTimer / gamePlayTimerMax);
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }
}
