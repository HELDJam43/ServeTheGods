using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    float currentGodRage = 0, maxGodRage = 100;
    public HUD hud;
    public GameOverUI ui;
    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start()
    {
        Instance.hud.SetRageMeter(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GodOrderFailed();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            GodOrderDelivered();
        }
    }
    public void Lose()
    {
        GameStateManager.SetState(GameStateManager.GameState.GAMEOVER);
    }
    public static void GodOrderFailed()
    {
        Instance.currentGodRage += 20;
        Instance.hud.SetRageMeter(Instance.currentGodRage / Instance.maxGodRage);
        if (Instance.currentGodRage >= Instance.maxGodRage)
        {
            Instance.Lose();
        }
    }

    public static void GodOrderDelivered()
    {
        Instance.currentGodRage -= 10;
        if (Instance.currentGodRage < 0)
            Instance.currentGodRage = 0;
        Instance.hud.SetRageMeter(Instance.currentGodRage / Instance.maxGodRage);
    }
}
