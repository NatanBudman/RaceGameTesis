using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(OptimizatedUpdateGameplay))]
public class InputManager : MonoBehaviour,IOptimizatedUpdate
{
    public KartControls Controls;
    public TurboManager _turboManager;
    public KartController _KartController;
    #region MoveInputs

       private KeyCode MovLeft => Controls.Left;
       private KeyCode MovRight => Controls.Right;
       public KeyCode MovForward => Controls.Forward;
       public KeyCode MovReverse => Controls.Reverse;

    #endregion

    #region ActionInputs

      private KeyCode PowerActive => Controls.Power;
      private KeyCode SkillActivate;
      public KeyCode JumpActive => Controls.Jump;
      public KeyCode TurboActive => Controls.Turbo;
      public KeyCode DriftActive => Controls.Drift;

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
