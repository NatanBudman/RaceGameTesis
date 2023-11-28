using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Controls" , menuName = "NewControl")]
public class KartControls : ScriptableObject
{
   public KeyCode Forward = KeyCode.W;
   public KeyCode Reverse = KeyCode.S;
   public KeyCode Left = KeyCode.A;
   public KeyCode Right = KeyCode.D;
   
   public KeyCode Jump = KeyCode.J;
   public KeyCode Turbo = KeyCode.LeftShift;
   public KeyCode Drift = KeyCode.Space;
   public KeyCode Power = KeyCode.K;
}
