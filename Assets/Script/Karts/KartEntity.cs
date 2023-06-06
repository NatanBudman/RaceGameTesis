using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartEntity : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    public KartStats _kartStats;

    #region KartStats

        private float SpeedRotate => _kartStats.SteerDirSpeed;
        private float Speed => _kartStats.MaxSpeed;

        public float RealSpeed;
        public float RealSpeedRotate;


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

    public float GetRealSpeed => RealSpeed;
    public float GetSpeed => Speed;

    public void SetRealSpeed(float Speed) 
    {
        RealSpeed = Speed;
    }
    public float GetRealSpeedRotate => RealSpeedRotate;
    public float GetSpeedRotate => SpeedRotate;

    private bool isCatch = true;

    public void CatchKart(bool isCachted) 
    {
        isCatch = isCachted;
    }

    public bool isKartCatch() => isCatch;
}
