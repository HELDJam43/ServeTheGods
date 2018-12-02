using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public enum StarRank
    {
        ZERO = 0,
        ONE = 100,
        TWO = 150,
        THREE = 250,
        FOUR = 450,
        FIVE = 600
    }
    public static LevelManager Instance;
    float currentGodRage = 0, maxGodRage = 100;
    int reviewRank = (int)StarRank.TWO;
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
        Instance.hud.SetReviewValue(Instance.CalcStars());
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            CustomerOrderFailed();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            CustomerOrderDelivered();
        }
    }
    public void Lose()
    {
        GameStateManager.SetState(GameStateManager.GameState.GAMEOVER);
        StartCoroutine(ShowGameOverScreen());
    }
    public static void GodOrderFailed()
    {
        if (GameStateManager.State == GameStateManager.GameState.GAMEOVER) return;
        Instance.currentGodRage += 20;
        Instance.hud.SetRageMeter(Instance.currentGodRage / Instance.maxGodRage);
        if (Instance.currentGodRage >= Instance.maxGodRage)
        {
            Instance.Lose();
        }
    }

    public static void GodOrderDelivered()
    {
        if (GameStateManager.State == GameStateManager.GameState.GAMEOVER) return;
        Instance.currentGodRage -= 10;
        if (Instance.currentGodRage < 0)
            Instance.currentGodRage = 0;
        Instance.hud.SetRageMeter(Instance.currentGodRage / Instance.maxGodRage);
    }

    public static void CustomerOrderFailed()
    {

        if (GameStateManager.State == GameStateManager.GameState.GAMEOVER) return;
        Instance.reviewRank -= 20;
        if (Instance.reviewRank < 50)
        {
            Instance.Lose();
        }
        Instance.hud.SetReviewValue(Instance.CalcStars());
    }

    public static void CustomerOrderDelivered()
    {
        if (GameStateManager.State == GameStateManager.GameState.GAMEOVER) return;
        Instance.reviewRank += 10;
        Instance.hud.SetReviewValue(Instance.CalcStars());
    }
    public int CalcStars()
    {
        int temp = Instance.reviewRank;
        int low, mid, high;
        int roundScore = 0;
        if (temp < (int)StarRank.ZERO)
        {
            return 0;
        }
        else if (temp < (int)StarRank.ONE)
        {
            low = (int)StarRank.ZERO;
            high = (int)StarRank.ONE;
            roundScore = 0;
        }
        else if (temp < (int)StarRank.TWO)
        {
            low = (int)StarRank.ONE;
            high = (int)StarRank.TWO;
            roundScore = 2;
        }
        else if (temp < (int)StarRank.THREE)
        {
            low = (int)StarRank.TWO;
            high = (int)StarRank.THREE;
            roundScore = 4;
        }
        else if (temp < (int)StarRank.FOUR)
        {
            low = (int)StarRank.THREE;
            high = (int)StarRank.FOUR;
            roundScore = 6;
        }
        else if (temp < (int)StarRank.FIVE)
        {
            low = (int)StarRank.FOUR;
            high = (int)StarRank.FIVE;
            roundScore = 8;
        }
        else
        {
            return 10;
        }
        mid = (high + low) / 2;
        if (temp >= mid)
            roundScore++;
        //Debug.LogWarning("RAW: " + reviewRank + " Converted:" + roundScore + "   Mid:" + mid);
        return roundScore;

    }
    IEnumerator ShowGameOverScreen()
    {
        float t = 0;
        float duration = .5f;
        CanvasGroup c = ui.GetComponent<CanvasGroup>();
        while (t < duration)
        {
            c.alpha = Mathf.Lerp(0, 1, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
