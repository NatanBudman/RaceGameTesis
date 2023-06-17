using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsOptions : MonoBehaviour
{
    public GameSettings Settings;

    public Text currentFPS;

    private void Start()
    {
        currentFPS.text = "" + Settings.GameplayFPS;
    }

    public void SetFPS(int Amount)
    {
        if (Amount < 0 && Settings.GameplayFPS > 30)
        {
            Settings.GameplayFPS -= Mathf.Abs(Amount);
        }
        else if(Amount > 0 && Settings.GameplayFPS < 90)
        {
            Settings.GameplayFPS += Amount;
        }

        currentFPS.text = "" + Settings.GameplayFPS;
    }
}
