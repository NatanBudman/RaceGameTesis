using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Settings", menuName = "RaceSettings")]
public class GameSettings : ScriptableObject
{
  #region FPS

    public int GameplayFPS;
    public int UXFPS;

  #endregion

  #region Race

    public int Bots;
    
    public int BotsDificult;
 
    public int Racelaps;
    
    public int BoxCooldown;

    public bool LostBoost;

    #endregion

    #region SetPlayer

    public KartStats playerStats;

    #endregion
}
