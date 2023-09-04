using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public Text lapTimesText;
    public KartEntity kartEntity;
    public GameManager gameManager;

    public int MaxTurning => gameManager.RaceLaps;

    private float lapStartTime;
    private float lapTime;
    private int lapCount;
    private bool lapStarted = false;
    private List<float> lapTimes = new List<float>();
    public int pointsInRace;

    private void Update()
    {
        if (lapStarted)
        {
            lapTime += Time.deltaTime;
            timerText.text = FormatTime(lapTime);
        }
    }

    private void OnTriggerEnter(Collider carCollider)
    {
        KartEntity kart = carCollider.GetComponent<KartEntity>();
        if (kart != null && carCollider.isTrigger == false && kart.gameObject.tag == "Player")
        {
            if (!lapStarted)
            {
                StartLap();
            }
            
        }
    }

    public void StartLap()
    {
        lapStarted = true;
        lapStartTime = lapTime;
    }

    public void SaveLapTime()
    {
        float lapElapsedTime = lapTime - lapStartTime;
        lapTimes.Add(lapElapsedTime);
    }

    public void StopTimer()
    {
        lapStarted = false;
        timerText.text = "Race Completed";
        timerText.enabled = false;
    }

    public void ShowLapTimes()
    {
        lapTimesText.text = "Lap Times:\n";
        for (int i = 0; i < lapTimes.Count - 1; i++) 
        {
            lapTimesText.text += "Vuelta " + (i + 1) + ": " + FormatTime(lapTimes[i]) + "\n";
        }
        
    }

    public string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
