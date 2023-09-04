using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMap : MonoBehaviour
{
    [Header("Game Pameters")]
    public int Runners;
    public int Laps;
    public int BoxSpawn;
    public int Dificulty;


    [Header(" Pameters Clamp")]
    public int MaxRunners;
    public int MaxLaps;
    public int MaxBoxSpawn;
    public int MaxDificulty;


    [Header(" See Pameters")]
    public Text RunnersTxt;
    public Text LapsTxt;
    public Text BoxTxt;
    public Text DiffTxt;
    [Space]
    [Space]
    [Space]
    public string Map;
    private void Start()
    {
        RunnersTxt.text = "" + Runners;
        LapsTxt.text = "" + Laps;
        BoxTxt.text = "" + BoxSpawn;
        SetDificult(0);
    }
    public void SetRunners(int positive) 
    {

        if (positive > 0 && Runners < MaxRunners)
        {
            Runners++;
        }
        else if (positive < 0 && Runners >= 1)
        {
            Runners--;
        }

        RunnersTxt.text = "" + Runners;
    }
    public void SetLaps(int positive) 
    {
        if (positive > 0 && Laps < MaxLaps)
        {
            Laps++;
        }
        else if(positive < 0 && Laps >= 1)
        {
            Laps--;
        }

        LapsTxt.text = "" + Laps;

    }
    public void SetSpawn(int positive) 
    {
        if (positive > 0 && BoxSpawn < MaxBoxSpawn)
        {
            BoxSpawn++;
        }
        else if (positive < 0 && BoxSpawn >= 3)
        {
            BoxSpawn--;
        }

        BoxTxt.text = "" + BoxSpawn;
    }
    public void SetDificult(int positive) 
    {
        if (positive > 0)
        {
            Dificulty++;
        }
        else if ( positive <= 0) 
        {

            Dificulty--;
        }
  

        switch (Dificulty)
        {
            case -1:
                DiffTxt.text = "Hard";
                Dificulty = 2;
                break;
            case 0:
                DiffTxt.text = "Easy";
                break;

            case 1:
                DiffTxt.text = "Normal";
                break;

            case 2:
                DiffTxt.text = "Hard";
                break;
            default:
                DiffTxt.text = "Easy";
                Dificulty = 0;
                break;
        }
    }

    public void SetMap(string SceneMap) 
    {
        Map = SceneMap;
    }
}
