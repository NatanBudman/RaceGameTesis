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
    public Collider carCollider;
    private bool lapCompleted = false;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (lapStarted == true)
        {
            lapTime += Time.deltaTime;
            timerText.text = FormatTime(lapTime);
        }
    }

    private void OnTriggerEnter(Collider carCollider)
    {
        InputManager car = carCollider.GetComponent<InputManager>();
        if (car != null && carCollider.isTrigger == false)
        {
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
                    lapCompleted = true;
                    if (lapCount >= gameManager.RaceLaps)
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
    }

    public void StartLap()
    {
        lapStarted = true;
        lapStartTime = lapTime;
        lapCompleted = false;
    }

    private void SaveLapTime()
    {
        float lapElapsedTime = lapTime - lapStartTime;
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

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
