using UnityEngine;

public class TurboManager : MonoBehaviour
{
    public KartEntity kart;
   [SerializeField] private int TurboSpeed;
   
   [Header("Turbo Bar")]
   [SerializeField] public float turboAmount;
   [SerializeField] private float ConsumeTurboAmount;
   [SerializeField] private float MaxTurboAmount;
   [SerializeField] private float TurboAmountGet;
   private void Start()
   {
        TurboSpeed += (int)kart.GetMaxRealSpeed;
   }
    public float GetTurboAmount() 
    {
        return turboAmount;
    }
    public float GetMaxTurboAmount() 
    {
        return MaxTurboAmount;
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
      return kart.GetVelocity > 1;
   }

   public void Turbo(bool Activate)
   {
      if (Activate && isHasTurboToUse() && isKartAcceleration())
      {
            if (kart.isKartCatch() == false) 
            {
                turboAmount -= ConsumeTurboAmount;
                kart.SetRealSpeed(TurboSpeed);
            }
       

      }
      else if (!Activate || !isKartAcceleration() || !isHasTurboToUse())
      {
            if (kart.isKartCatch() == false) 
            {
                kart.SetRealSpeed(kart.GetSpeed);
            }
                
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
