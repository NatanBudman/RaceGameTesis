using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartUI : MonoBehaviour, IOptimizatedUpdate
{
    public GameManager Manager;
    public TurboManager turbo;
    public KartEntity entity;
    public CoinsUIManager CoinsManager;
    // public PositionStack stackPos;
    [Space]
    [Header("StartCount")]
    public Image One;
    public Image Two;
    public Image Three;
    public Image Go;
    public GameObject Pause;
    private delegate void StartTimer();

    private event StartTimer OnTimerCurrent;
    private float curr;
    [Space]
    [Header("Laps")]
    public Text currLaps;
    public Text Laps;


    [Header("Turbo")]
    public Image TurboAmount;

    [Header("Coins")]
    public Text Coins;
    private int coins;
    public int currentLaps = 1;
    [HideInInspector] public int currentTotalLaps;
    private void Start()
    {
        entity = GetComponent<KartEntity>();
        OnTimerCurrent += TimerCurrent;
        Laps.text = "" + Manager.RaceLaps;
        UpdateLaps();
    }

    void TimerCurrent()
    {
        curr = Manager.CurrRaceTimer;
        if (curr > 0 && curr <= 1)
        {
            One.gameObject.SetActive(true);
            Two.gameObject.SetActive(false);

        }
        if (curr > 1 && curr <= 2)
        {
            Two.gameObject.SetActive(true);
            Three.gameObject.SetActive(false);


        }
        if (curr > 2 && curr <= 3)
        {
            Three.gameObject.SetActive(true);
        }
        else if (curr <= 0)
        {
            Go.gameObject.SetActive(true);
            One.gameObject.SetActive(false);

            OnTimerCurrent -= TimerCurrent;
        }
    }
    public void UpdateLaps()
    {
        currLaps.text = "" + currentLaps;

    }
    void UpdateTurboBar()
    {
        TurboAmount.fillAmount = turbo.GetTurboAmount() / turbo.GetMaxTurboAmount();
    }
    private void Update()
    {
        UpdateTurboBar();
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnPause(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause(true);
        }
    }
    public void Op_UpdateGameplay()
    {
      
    }

   public void Op_UpdateUX()
   {
      if (OnTimerCurrent != null)
      {
         OnTimerCurrent();
      }
      else
      {
            Invoke("Go_text", 2);
      }
      if (coins != entity.Coins) 
      {
          Coins.text ="" + entity.Coins;
          CoinsManager.GetCoins(entity.Coins - coins);
          coins = entity.Coins;
      }
     //   CurrentPos.text = $"{this.gameObject.name}  Position : " + stackPos.GetPos(this.gameObject);
   }

    void Go_text() 
    {
        Go.gameObject.SetActive(false);
    }

    public void OnPause(bool pause) 
    {
        if (pause)
        {
            Time.timeScale = 0;
            Pause.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1;
            Pause.SetActive(false);

        }
    }
}
