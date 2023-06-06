using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
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

    private void Start()
    {
        StartCoroutine(InitRace());
    }
    IEnumerator InitRace() 
    {

        for (int i = 0; i < KartsInGame.Length; i++)
        {
            KartEntity kart = KartsInGame[i].GetComponent<KartEntity>();
            kart.CatchKart(true);
            kart.SetRealSpeed(0);
        }

        yield return new WaitForSeconds(StartRaceTimer);


        for (int i = 0; i < KartsInGame.Length; i++)
        {
            KartEntity kart = KartsInGame[i].GetComponent<KartEntity>();

            kart.CatchKart(false);
            kart.SetRealSpeed(kart.GetSpeed);
            Debug.Log("entre");

        }
        StopCoroutine(InitRace());
    }
}
