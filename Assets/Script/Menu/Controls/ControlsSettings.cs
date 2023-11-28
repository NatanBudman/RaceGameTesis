using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsSettings : MonoBehaviour
{
   public KartControls KartControls;
   private Text keyName;
   private bool isCanSelectKey;
   private string KeySelected;


   [Space] 
   public Text Foward;
   public Text Reverse;
   public Text Left;
   public Text Right;
   public Text Jump;
   public Text Power;
   public Text Turbo;
   public Text Drift;
   public void GetKeyButton(Text KeyString)
   {
      keyName = KeyString;
      isCanSelectKey = true;
   }

   public void GetKeyName(String Key)
   {
      KeySelected = Key;
      Debug.Log("presionado");
   }

   public void DefaultControls()
   {
      SetKeys("Default", pressKey);
   }

   private KeyCode pressKey = KeyCode.Insert;
   private void OnGUI()
   {
      if (isCanSelectKey && Event.current.isKey)
      {
         pressKey = Event.current.keyCode;
         keyName.text = pressKey.ToString();
         SetKeys(KeySelected, pressKey);
         keyName = null;
         isCanSelectKey = false;
      }
   }
   private void SetKeys(String KeyName , KeyCode Key)
   {
      switch (KeyName)
      {
         case "Forward":
            KartControls.Forward = Key;
            break;
         case "Reverse":
            KartControls.Reverse = Key;
            break;
         case "Left":
            KartControls.Left = Key;
            break;
         case "Right":
            KartControls.Right = Key;
            break;
         
         case "Jump":
            KartControls.Jump = Key;
            break;
         case "Drift":
            KartControls.Drift = Key;
            break;
         case "Power":
            KartControls.Power= Key;
            break;
         case "Turbo":
            KartControls.Turbo = Key;
            break;
         case "Default":
            
            KartControls.Forward = KeyCode.W;
            Foward.text = "W";
            
            KartControls.Reverse = KeyCode.S;
            Reverse.text = "S";

            KartControls.Left = KeyCode.A;
            Left.text = "A";

            KartControls.Right = KeyCode.D;
            Right.text = "D";


            KartControls.Jump = KeyCode.J;
            Jump.text = "J";

            KartControls.Drift = KeyCode.Space;
            Drift.text = "SPACE";

            KartControls.Power= KeyCode.K;
            Power.text = "K";

            KartControls.Turbo = KeyCode.LeftShift;
            Turbo.text = "SHIFT";

            break;
         
      }
   }

}
