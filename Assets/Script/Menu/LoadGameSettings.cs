using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    bool isStartRace = false;
    float currCount = 4;
    string TrackName => Map.Map;
    private Text CurrentCountTXT;
    private void Update()
    {
        if (isStartRace)
        {
            currCount -= Time.deltaTime;
            CurrentCountTXT.text = "" + (int)currCount;
            if (currCount <= 0) 
            {
                SceneManager.LoadScene($"Scenes/{TrackName}");
            }
        }
        
    }
    public void StartRaceCount(Text countText) 
    {
        CurrentCountTXT = countText;
        isStartRace = true;
    }
    public void CancelStartRace() 
    {
        isStartRace = false;
        currCount = 4;
    }
}
