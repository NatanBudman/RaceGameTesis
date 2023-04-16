using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboManager : MonoBehaviour
{
   public KartController _kart;
   private float Speed;
   [SerializeField] private int TurboSpeed;
   
   [Header("Turbo Bar")]
   [SerializeField] private float turboAmount;
   [SerializeField] private float ConsumeTurboAmount;
   [SerializeField] private float MaxTurboAmount;
   [SerializeField] private float TurboAmountGet;
   private void Start()
   {
      Speed = _kart.maxSpeed;
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
      return _kart.realSpeed > 1;
   }

   public void Turbo(bool Activate)
   {
      if (Activate && isHasTurboToUse() && isKartAcceleration())
      {
         turboAmount -= ConsumeTurboAmount;
         _kart.maxSpeed = Speed + TurboSpeed;
         _kart.realSpeed = _kart.maxSpeed;
      }
      else if (!Activate || !isKartAcceleration() || !isHasTurboToUse())
      {
         _kart.maxSpeed = Speed;
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
