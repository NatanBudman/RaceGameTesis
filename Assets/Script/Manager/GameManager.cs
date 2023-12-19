using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>,IOptimizatedUpdate
{
    [Header("SettingsGame")] 

    public GameSettings Settings;

    public int GameplayFPS => Settings.GameplayFPS;
    public int UXFPS => Settings.UXFPS;
  
    [Space]
    [Header("Karts")] 
    [Space]

    public KartStats[] StatsArray;

    int karts => Settings.Bots;
    public GameObject[] KartsInGame;
    public GameObject[] BotsInGame;

    [Space]
    public int StartRaceTimer;
    public float speedStartRace;
    [HideInInspector]  public float CurrRaceTimer;
    public int RaceLaps => Settings.Racelaps;

    public GameObject[] Points;

    private delegate void StartRace();

    private event StartRace OnStartRace;
    
        
    public KartStats PlayerStats => Settings.playerStats;



    public override void Awake()
    {
        base.Awake();

        int bots = karts;
        for (int i = 0; i < BotsInGame.Length; i++) 
        {
            if (bots <= 0) continue;

            BotsInGame[i].SetActive(true);

            bots -= 1;
        }

        Time.timeScale = 1;
        CurrRaceTimer = StartRaceTimer;

        OnStartRace += StartRacing;
    }
    private void Start()
    {
        Time.timeScale = 1;
        KartEntity[] kart = FindObjectsOfType<KartEntity>();

        for (int i = 0; i < kart.Length; i++)
        {
            KartsInGame[i] = kart[i].gameObject;
        }
    }
    void StartRacing()
    {
        CurrRaceTimer -= Time.deltaTime * speedStartRace;

        if (CurrRaceTimer <= 0)
        {
            for (int i = 0; i < KartsInGame.Length; i++)
            {
                KartEntity kart = KartsInGame[i].GetComponent<KartEntity>();

                kart.CatchKart(false);
                kart.SetRealSpeed(kart.GetSpeed);

            }

            OnStartRace -= StartRacing;
        }
        else
        {
            for (int i = 0; i < KartsInGame.Length; i++)
            {
                KartEntity kart = KartsInGame[i].GetComponent<KartEntity>();
                kart.CatchKart(true);
                kart.SetRealSpeed(0);
            }
        }
    }

    public void Op_UpdateGameplay()
    {
        if (OnStartRace != null)
            OnStartRace();
    }

    public void Op_UpdateUX()
    {
        throw new System.NotImplementedException();
    }
}
