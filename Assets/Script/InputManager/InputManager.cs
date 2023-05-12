using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(OptimizatedUpdateGameplay))]
public class InputManager : MonoBehaviour,IOptimizatedUpdate
{
    public TurboManager _turboManager;
    #region MoveInputs

       public KeyCode MovForward;
       private KeyCode MovLeft;
       private KeyCode MovRight;
       public KeyCode MovReverse;

    #endregion

    #region ActionInputs

      private KeyCode PowerActive;
      private KeyCode JumpActive;
      public KeyCode TurboActive = KeyCode.LeftShift;

      private KeyCode SkillActivate;

      public KeyCode DriftActive = KeyCode.Space;

    #endregion
    
    public void Op_UpdateGameplay()
    {
        if (Input.GetKey(TurboActive))
        {
            _turboManager.Turbo(true);
        }
        else
        {
            _turboManager.Turbo(false);
        }

        if (Input.GetKey(DriftActive))
        {
            _turboManager.GetTurbo();
        }
    }

    public void Op_UpdateUX()
    {
    }
}
