using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateManager
{

    public enum GameState
    {
        GAMEPLAY,
        MENU,
        DIALOUGE,
        PAUSE,
        CINEMA,
        TUTORIAL,
        GAMEOVER,
        NEXTLEVEL,
        STARTSCREEN,
        ANY
    }
    static GameStateManager()
    {
        SetState(GameState.GAMEPLAY);
    }
    static GameState state;
    public delegate void OnStateChangeEvent();
    public static OnStateChangeEvent StateChange;
    public static GameState State { get { return state; } }
    static float NormalTimeScale = 1;
    public static void SetState(GameState s)
    {
        state = s;
        UpdateTime();
        if (StateChange != null)
            StateChange();
    }
    public static void SetNormalTime(float x)
    {
        NormalTimeScale = x;
        UpdateTime();
    }
    public static void UpdateTime()
    {

        switch (state)
        {
            case GameState.STARTSCREEN:
                Time.timeScale = 0;
                AudioListener.pause = false;
                break;
            case GameState.GAMEPLAY:
                Time.timeScale = NormalTimeScale;
                AudioListener.pause = false;
                break;
            case GameState.MENU:
                Time.timeScale = NormalTimeScale;
                AudioListener.pause = true;
                break;
            case GameState.DIALOUGE:
                Time.timeScale = 0;
                AudioListener.pause = true;
                break;
            case GameState.PAUSE:
                Time.timeScale = 0;
                AudioListener.pause = true;
                break;
            case GameState.CINEMA:
                Time.timeScale = NormalTimeScale;
                AudioListener.pause = true;
                break;
            case GameState.TUTORIAL:
                Time.timeScale = 0;
                AudioListener.pause = true;
                break;
            case GameState.GAMEOVER:
                Time.timeScale = 1;
                AudioListener.pause = true;
                break;
            case GameState.NEXTLEVEL:
                Time.timeScale = 1;
                AudioListener.pause = true;
                break;
            default:
                Debug.LogError("You Done Fucked Up Now");
                break;
        }
    }

}
