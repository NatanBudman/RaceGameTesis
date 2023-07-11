using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour,IOptimizatedUpdate
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

    [Space]
    public int StartRaceTimer;
    [HideInInspector]  public float CurrRaceTimer;


    private delegate void StartRace();

    private event StartRace OnStartRace;
    
        
    public KartStats PlayerStats => Settings.playerStats;

    private void Awake()
    {
        Time.timeScale = 1;
//       GameObject player = FindObjectOfType<InputManager>().gameObject;

 //       player.GetComponent<KartEntity>()._kartStats = PlayerStats;

        KartsInGame = new GameObject[karts + 1];

        KartEntity[] kart = FindObjectsOfType<KartEntity>();

        for (int i = 0; i < kart.Length; i++) 
        {
            KartsInGame[i] = kart[i].gameObject;
        }

        CurrRaceTimer = StartRaceTimer;

        OnStartRace += StartRacing;
    }

    void StartRacing()
    {
        CurrRaceTimer -= Time.deltaTime * 1;

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
