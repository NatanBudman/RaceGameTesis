using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public KartController KartController;
    public GameObject objectToFollow;
    public float cameraHeight = 5f;
    // acerca camara
    public float maxSpeedDistance = 10;
    // aleja Camara
    public float minSpeedDistance = 5;
    public float minSpeedDistanceTurboInclude = 5;

    private float targetDistance;
//no tocar
    public float lerp;

    private float p ;
    // LateUpdate is called once per frame, after all other objects have been updated
    private void Start()
    {
        p = minSpeedDistance;
    }

    void LateUpdate ()
    {
        if (KartController.maxSpeed > KartController.StartVelocity)
        {
            minSpeedDistance = p + minSpeedDistanceTurboInclude;
        }
        else
        {
            minSpeedDistance = p ;
        }
        
        float speed = KartController.realSpeed;
        float speedRatio = Mathf.InverseLerp(0f, KartController.maxSpeed, speed);
        targetDistance = Mathf.Lerp(maxSpeedDistance, minSpeedDistance, speedRatio);
        Vector3 targetPosition = objectToFollow.transform.position - 
                                 objectToFollow.transform.forward * targetDistance + 
                                 Vector3.up * cameraHeight;
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerp);
        transform.LookAt(objectToFollow.transform.position);
    }
  
}
