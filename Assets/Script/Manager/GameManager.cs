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
  
  public KartStats PlayerStats => Settings.playerStats;

    private void Awake()
    {
        GameObject player = FindObjectOfType<InputManager>().gameObject;

        player.GetComponent<KartEntity>()._kartStats = PlayerStats;
    }
}
