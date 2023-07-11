using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartUI : MonoBehaviour,IOptimizatedUpdate
{
   public GameManager Manager;
   public Text currTime;

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
      if (curr >= 0)
      {
         currTime.text = "" + Mathf.Ceil(curr);
      }
      else
      {
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
         if (currTime.gameObject.activeInHierarchy)
         {
            currTime.gameObject.SetActive(false);
         }
      }

   }
}
