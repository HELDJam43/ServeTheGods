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
        if (StateChange != null)
            StateChange();
    }

}
