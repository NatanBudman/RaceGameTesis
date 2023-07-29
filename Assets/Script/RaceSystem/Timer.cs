using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timerText;
    public Text lapTimesText;
    public InputManager carScript;
    private float lapStartTime;
    private float lapTime;
    private int lapCount;
    private bool lapStarted = false;
    private List<float> lapTimes = new List<float>();

    private void Update()
    {
        if (lapStarted == true)
        {
            lapTime = Time.time;
            timerText.text = FormatTime(lapTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InputManager car = other.GetComponent<InputManager>();

        if (car != null)
        {
            if (!lapStarted)
            {
                StartLap();

            }
            else
            {
                SaveLapTime();
                lapCount++;

                if (lapCount >= 3)
                {
                    StopTimer();
                    ShowLapTimes();
                }
                else
                {
                    StartLap();
                }
            }
        }
    }

    private void StartLap()
    {
        lapStarted = true;
        lapStartTime = Time.time;
    }

    private void SaveLapTime()
    {
        float lapElapsedTime = Time.time - lapStartTime;
        lapTimes.Add(lapElapsedTime);
    }

    private void StopTimer()
    {
        lapStarted = false;
        timerText.text = "Race Completed";
        timerText.enabled = false;
    }

    private void ShowLapTimes()
    {
        lapTimesText.text = "Lap Times:\n";
        for (int i = 0; i < lapTimes.Count; i++)
        {
            lapTimesText.text += "Lap " + (i + 1) + ": " + FormatTime(lapTimes[i]) + "\n";
        }
    }



    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        float milliseconds = (time * 1000) % 1000;

        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
