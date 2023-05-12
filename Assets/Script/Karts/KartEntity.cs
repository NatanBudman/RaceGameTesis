using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartEntity : MonoBehaviour
{
    [SerializeField] private GameManager _manager;
    [SerializeField] private Rigidbody _rb;
    private KartStats _kartStats;

    #region KartStats

        public float SpeedRotate => _kartStats.SteerDirSpeed;
        public float Speed => _kartStats.MaxSpeed;


    #endregion

    public Vector3 GetForward => transform.forward;

    public float GetVelocity => _rb.velocity.magnitude;
      
    
     public void LookRotate(Vector3 dir)
      {

          Quaternion targetRotation = Quaternion.LookRotation(dir);

          transform.transform.rotation = Quaternion.RotateTowards(transform.transform.rotation,
              targetRotation, SpeedRotate * Time.deltaTime);
      }

}
