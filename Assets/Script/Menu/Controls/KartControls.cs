using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Controls" , menuName = "NewControl")]
public class KartControls : ScriptableObject
{
   public KeyCode Forward;
   public KeyCode Reverse;
   public KeyCode Left;
   public KeyCode Right;
   
   public KeyCode Jump;
   public KeyCode Turbo;
   public KeyCode Drift;
   public KeyCode Power;
}
