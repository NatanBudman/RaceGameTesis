using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(OptimizatedUpdateGameplay))]
public class InputManager : MonoBehaviour,IOptimizatedUpdate
{
    public TurboManager _turboManager;
    public KartController _KartController;
    #region MoveInputs

       private KeyCode MovLeft;
       private KeyCode MovRight;
       public KeyCode MovForward;
       public KeyCode MovReverse;

    #endregion

    #region ActionInputs

      private KeyCode PowerActive;
      private KeyCode SkillActivate;
      public KeyCode JumpActive = KeyCode.J;
      public KeyCode TurboActive = KeyCode.LeftShift;
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

        if (Input.GetKeyDown(JumpActive) && _turboManager.turboAmount >= 50 && _KartController.isGrounded)
        {
            Debug.Log("salto");
            _turboManager.turboAmount -= 50;
            _KartController.Jump();
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
