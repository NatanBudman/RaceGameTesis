using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartEntity : MonoBehaviour
{
    [SerializeField] private GameManager _manager;
    [SerializeField] private Rigidbody _rb;
    public KartStats _kartStats;

    #region KartStats

        public float SpeedRotate => _kartStats.SteerDirSpeed;
        public float Speed => _kartStats.MaxSpeed;


    #endregion

    #region KartGameplay

    [SerializeField]private int currentPoint;
    [SerializeField]private int currentTurning;

    #endregion
    public Vector3 GetForward => transform.forward;

    public float GetVelocity => _rb.velocity.magnitude;
      
    
     public void LookRotate(Vector3 dir)
      {

          Quaternion targetRotation = Quaternion.LookRotation(dir);

          transform.transform.rotation = Quaternion.RotateTowards(transform.transform.rotation,
              targetRotation, SpeedRotate * Time.deltaTime);
      }

    public void SetPoint(int point) 
    {
        currentPoint = point;
    }

    public bool isNextCurrentPoint(int point) 
    {
        if (currentPoint == point) { return true; } else { return false; }
    }

    public int GetCurrentPoint() => currentPoint;

    public void SetTurning(int Turning)
    {
        currentTurning = Turning;
    }

    public bool isNextCurrentTurning(int Turning)
    {
        if (currentTurning == Turning) { return true; } else { return false; }
    }

    public int GetCurrentTurning() => currentTurning;
}
