using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartUI : MonoBehaviour,IOptimizatedUpdate
{
   public GameManager Manager;
   public Image One;
   public Image Two;
   public Image Three;
   public Image Go;

   private delegate void StartTimer();

   private event StartTimer OnTimerCurrent;
   private float curr;
   private void Start()
   {
      OnTimerCurrent += TimerCurrent;
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
      else if(curr <= 0)
      {
            Go.gameObject.SetActive(true);
            One.gameObject.SetActive(false);

            OnTimerCurrent -= TimerCurrent;
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

   }

    void Go_text() 
    {
        Go.gameObject.SetActive(false);

    }

}
