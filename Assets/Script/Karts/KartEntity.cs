using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartEntity : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    public KartStats _kartStats;

    #region KartStats

        public float SpeedRotate => _kartStats.SteerDirSpeed;
        public float Speed => _kartStats.MaxSpeed;

        private float CurrentSpeed;

        private float CurrentRotate;
    #endregion

    #region KartGameplay

    [SerializeField]private int currentPoint;
    [SerializeField]private int currentTurning;

    #endregion
    public Vector3 GetForward => transform.forward;

    public float GetVelocity => _rb.velocity.magnitude;

    private void Start()
    {
        CurrentSpeed = Speed;
        CurrentRotate = SpeedRotate;
    }
    public void LookRotate(Vector3 dir)
      {

          Quaternion targetRotation = Quaternion.LookRotation(dir);

          transform.transform.rotation = Quaternion.RotateTowards(transform.transform.rotation,
              targetRotation, SpeedRotate * Time.deltaTime);
      }

    bool isCatchKart = true;
    public void KartStop(bool isCatch) 
    {

        if (isCatch) CurrentSpeed = 0;
        else CurrentSpeed = Speed;

        isCatchKart = isCatch;
    }

    public void SetCurrentSpeed(float Amount) 
    {
        CurrentSpeed = Amount;
    }
    public float GetNormalSpeed() => Speed;
    public bool isCatch() 
    {
        return isCatchKart;
    }
    public float GetKartSpeed() => CurrentSpeed;
    public float GetKartSpeedRotate() => CurrentRotate;

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
