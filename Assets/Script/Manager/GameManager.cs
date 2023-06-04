using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IOptimizatedUpdate
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
  
  public KartStats PlayerStats => Settings.playerStats;

    private void Awake()
    {
        GameObject player = FindObjectOfType<InputManager>().gameObject;

        player.GetComponent<KartEntity>()._kartStats = PlayerStats;

        KartsInGame = new GameObject[karts + 1];

        KartEntity[] kart = FindObjectsOfType<KartEntity>();

        for (int i = 0; i < kart.Length; i++) 
        {
            KartsInGame[i] = kart[i].gameObject;
        }
       
    }


    [SerializeField] private float StartCounter;
    private float CurrentStartCounter = 0;

    public void Op_UpdateGameplay()
    {
        CurrentStartCounter += Time.deltaTime;
     
        if (CurrentStartCounter >= StartCounter)
        {
           
            foreach (GameObject karts in KartsInGame)
            {

                KartEntity kart = karts.GetComponent<KartEntity>();

                if (kart.isCatch()) { kart.KartStop(false); }
            }
        }
        else 
        {
            for (int i = 0; i < KartsInGame.Length; i++) 
            {
                KartsInGame[i].GetComponent<KartEntity>().KartStop(true);
            }
            
        }
    }

    public void Op_UpdateUX()
    {
        throw new System.NotImplementedException();
    }
}
