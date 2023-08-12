using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMeter : MonoBehaviour, IOptimizatedUpdate
{
    public float MaxSpeed;

    public GameObject SpeedMeterObject;
    private float RealSpeed => rigidbody.velocity.magnitude;
    public Rigidbody rigidbody;
    private float x;

    private void Start()
    {
        x = MaxSpeed / 2;
    }
    public void Op_UpdateGameplay()
    {
        throw new System.NotImplementedException();
    }
    public void Op_UpdateUX()
    {
        float Limiter = ((RealSpeed / MaxSpeed) * 100);
        float Limiter2 = ((Limiter / 90) * 100);
        float RotationZ = 90 - Limiter2;


        if (RealSpeed <= 1) RotationZ = 90;

        SpeedMeterObject.transform.rotation = Quaternion.Euler(0, 0, RotationZ);
    }
}
