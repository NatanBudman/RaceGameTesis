using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboManager : MonoBehaviour
{
    public KartEntity kart;
   [SerializeField] private float TurboSpeed;
   
   [Header("Turbo Bar")]
   [SerializeField] private float turboAmount;
   [SerializeField] private float ConsumeTurboAmount;
   [SerializeField] private float MaxTurboAmount;
   [SerializeField] private float TurboAmountGet;

    private float turbo;

    private void Start()
    {
        turbo = kart.GetKartSpeed() + TurboSpeed;
    }
    private bool isHasTurboToUse()
   {
      
      return turboAmount > 1;
   }

   private bool isHasMaxTurboAmount()
   {
      return MaxTurboAmount < turboAmount;
   }

   private bool isKartAcceleration()
   {
      return kart.GetComponent<Rigidbody>().velocity.magnitude > 1;
   }

   public void Turbo(bool Activate)
   {
      if (Activate && isHasTurboToUse() && isKartAcceleration())
      {
            turboAmount -= ConsumeTurboAmount;
            kart.SetCurrentSpeed(turbo);
      }
      else if (!Activate || !isKartAcceleration() || !isHasTurboToUse() && kart.GetKartSpeed() > 1)
      {
            kart.SetCurrentSpeed(kart.GetNormalSpeed());
      }
    
   }

   public void GetTurbo()
   {
      if (isKartAcceleration() && !isHasMaxTurboAmount())
      {
         turboAmount += TurboAmountGet;
      }
   }

}
