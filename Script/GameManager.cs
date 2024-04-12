using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    public event EventHandler OnPauseGame;
    public event EventHandler OnResumeGame;


    private void Awake()
    {
        Instance = this;
    }
    
    public enum State
    {
        WaittingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }
    private State state;
    private float timerWaittingToStart=1f;
    private float timerCountDownToStart = 3f;
    private float timerGamePlaying = 20f;
    private float timerGamePlayingMax = 20f;
    private bool isPause = false;
    private void Start()
    {
        state=State.WaittingToStart;
        GameInput.Instance.OnPauseGame += GameInput_OnPauseGame;
    }

    private void GameInput_OnPauseGame(object sender, EventArgs e)
    {
        togglePauseGame();
    }

    private void Update()
    {
        switch(state)
        {
            case State.WaittingToStart:
                timerWaittingToStart-=Time.deltaTime;
                if (timerWaittingToStart < 0f)
                {
                    state = State.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountDownToStart:

                timerCountDownToStart -= Time.deltaTime;
                if (timerCountDownToStart < 0f)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                    timerGamePlaying = timerGamePlayingMax;
                }
                break;
            case State.GamePlaying:
                timerGamePlaying -= Time.deltaTime;
                if (timerGamePlaying < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;
            case State.GameOver:
                break;
        }
    }
    public bool IsCountDownToStart()
    {
        return state== State.CountDownToStart;
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public float GetTimerCountDownToStart()
    {
        return timerCountDownToStart;
    }
    public bool IsGameOver() { return state== State.GameOver; }
    public float GetTimerGamePlayingNomalized() {
        return 1 - (timerGamePlaying / timerGamePlayingMax);
    }
    public void togglePauseGame()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0f;
            OnPauseGame?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnResumeGame?.Invoke(this, EventArgs.Empty);

        }
    }
}
