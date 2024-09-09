using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnGameStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameResumed;
    public static GameManager Instance { get; private set; }
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        Playing,
        GameOver,
    }

    private State state;
    private float countdownToStartTimer = 3f;
    private float gamePlayTimer;
    private float gamePlayTimerMax = 50f;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnGameStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
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

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameResumed?.Invoke(this, EventArgs.Empty);
        }
    }
}
