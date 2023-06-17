using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KartEntity : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    public KartStats _kartStats;

    #region KartStats

        private float SpeedRotate => _kartStats.SteerDirSpeed;
        private float Speed => _kartStats.MaxSpeed;

        public float MaxRealSpeed;
        
        public float MaxRealSpeedRotate;


    #endregion

    #region KartGameplay

    [SerializeField]private int currentPoint;
    [SerializeField]private int currentTurning;

    #endregion
    public Vector3 GetForward => transform.forward;

    public float GetVelocity => _rb.velocity.magnitude;

    private void Start()
    {
        MaxRealSpeedRotate = GetSpeedRotate;
    }

    public void LookRotate(Vector3 dir)
      {

          Vector3 direction = dir - transform.position;

          dir.y = 0;

          var rotation = Quaternion.LookRotation(dir);

          transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
          
          
      }


    #region Turnings
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

    #endregion


    public float GetMaxRealSpeed => MaxRealSpeed;
    public float GetSpeed => Speed;

    
    public float GetMaxRealSpeedRotate => MaxRealSpeedRotate;
    public float GetSpeedRotate => SpeedRotate;
    
    private bool isCatch = true;
    
    public bool isKartCatch() => isCatch;
    
    
    public void SetRealSpeed(float Speed) 
    {
        MaxRealSpeed = Speed;
    }
    public void CatchKart(bool isCachted) 
    {
        isCatch = isCachted;
    }

    
    
    
}
