using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartEntity : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float SpeedRotate;
    public float Speed;

    public Vector3 GetForward => transform.forward;

    public float GetVelocity => _rb.velocity.magnitude;
      
    
     public void LookRotate(Vector3 dir)
      {

          Quaternion targetRotation = Quaternion.LookRotation(dir);

          transform.transform.rotation = Quaternion.RotateTowards(transform.transform.rotation,
              targetRotation, SpeedRotate * Time.deltaTime);
      }

}
