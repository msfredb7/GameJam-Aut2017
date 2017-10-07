using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : PublicSingleton<Game>
{
    public static DelayedEvents DelayedEvents { get { return instance != null ? instance.delayedEvents : null; } }
    public static GameUI GameUI { get { return instance != null ? instance.gameUI : null; } }
    public static FAStar Fastar { get { return instance != null ? instance.map.fastar : null; } }
    public static HeroManager HeroManager { get { return instance != null ? instance.heroManager : null; } }
    public static Map Map { get { return instance != null ? instance.map : null; } }

    [SerializeField]
    private DelayedEvents delayedEvents;
    [SerializeField]
    private HeroManager heroManager;
    [SerializeField, ReadOnly]
    private GameUI gameUI;
    [SerializeField, ReadOnly]
    private Map map;


    // GAME STATE
    [ReadOnly]
    public bool gameStarted = false;
    [ReadOnly]
    public bool gameReady = false;
    [ReadOnly]
    public bool gameEnded = false;

    static private event SimpleEvent onGameReady;
    static private event SimpleEvent onGameStart;

    static public event SimpleEvent OnGameReady
    {
        add
        {
            if (instance != null && instance.gameReady)
                value();
            else
                onGameReady += value;
        }
        remove
        {
            onGameReady -= value;
        }
    }

    static public event SimpleEvent OnGameStart
    {
        add
        {
            if (instance != null && instance.gameStarted)
                value();
            else
                onGameStart += value;
        }
        remove
        {
            onGameStart -= value;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onGameReady = null;
        onGameStart = null;
    }

    public void PrepareLaunch(GameUI gameUI, Map map)
    {
        this.gameUI = gameUI;
        this.map = map;

        //Ready up !
        ReadyGame();

        //Intro ?
        //Si NON
        StartGame();
    }

    void ReadyGame()
    {
        if (gameReady)
            return;

        gameReady = true;

        Debug.Log("Game ready");

        LoadingScreen.OnNewSetupComplete();
        delayedEvents.enabled = true;

        if (onGameReady != null)
        {
            onGameReady();
            onGameReady = null;
        }
    }

    void StartGame()
    {
        if (gameStarted)
            return;

        gameStarted = true;

        Debug.Log("Game started");

        // Init Game Start Events
        if (onGameStart != null)
        {
            onGameStart();
            onGameStart = null;
        }
    }

    public void Win()
    {
        if (gameEnded)
            return;
        gameEnded = true;

        NotificationQueue.PushNotification("You won");
    }

    public void Lose()
    {
        if (gameEnded)
            return;
        gameEnded = true;

        NotificationQueue.PushNotification("You lost");
    }
}
