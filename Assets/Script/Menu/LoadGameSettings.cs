using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameSettings : MonoBehaviour
{
    public GameSettings GameSettings;

    public KartChoise kartChoise;

    public SettingMap Map;

    public void LoadSettings() 
    {
        #region Map

        GameSettings.Bots = Map.Runners;
        GameSettings.BoxCooldown = Map.BoxSpawn;
        GameSettings.Racelaps = Map.Laps;
        GameSettings.BotsDificult = Map.Dificulty;
        #endregion

        #region Kart

        GameSettings.playerStats = kartChoise.CurrentStats;
        #endregion
    }
}
