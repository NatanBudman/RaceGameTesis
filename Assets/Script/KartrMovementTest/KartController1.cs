using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController1 : MonoBehaviour
{

    // Variables para ajustar la velocidad del kart.
    public float speed = 20f;
    public float reverseSpeed = 10f;
    public float maxSpeed = 50f;
    public float acceleration = 60f;
    public float deacceleration = 60f;

    // Variables para ajustar el movimiento de derrape.
    public float driftFactor = .95f;
    public float driftTendency = .5f;
    public float steerSpeed = 80f;

    // Variables para activar el drift.
    public KeyCode driftKey = KeyCode.Space;
    public bool isDrifting = false; 

    // Variable para verificar si el kart está en el aire.
    private bool grounded;

    // Componentes para los colliders de las ruedas.
    private WheelCollider[] wheels;
    private Transform[] wheelMeshes;

    private void Start()
    {
        // Obtener los colliders de las ruedas y sus transformaciones.
        wheels = GetComponentsInChildren<WheelCollider>();
        wheelMeshes = new Transform[wheels.Length];

        for (int i = 0; i < wheelMeshes.Length; i++)
        {
            wheelMeshes[i] = wheels[i].GetComponentInChildren<MeshRenderer>().transform;
        }
    }

private void FixedUpdate()
{
    float motor = 0;
    float steer = Input.GetAxis("Horizontal") * steerSpeed;

    // Verificar si el jugador está presionando la tecla para activar el drift.
    if (Input.GetKeyDown(driftKey))
    {
        isDrifting = true;
    }
    else if (Input.GetKeyUp(driftKey))
    {
        // Detener el drift.
        isDrifting = false;
    }

    // Ajustar la velocidad del kart dependiendo de la dirección en la que se mueve.
    if (Input.GetAxis("Vertical") > 0)
    {
        motor = Input.GetAxis("Vertical") * acceleration;
    }
    else if (Input.GetAxis("Vertical") < 0)
    {
        motor = Input.GetAxis("Vertical") * deacceleration;
    }

    motor = Mathf.Clamp(motor, -reverseSpeed, maxSpeed);

    // Aplicar la dirección.
    if (grounded)
    {
        transform.Rotate(0, steer, 0);
    }

// Ajustar la velocidad de las ruedas.
    foreach (WheelCollider wheel in wheels)
    {
        wheel.motorTorque = motor;
        wheel.brakeTorque = 0;

        // Activar el derrape.
        var wheelSidewaysFriction = wheel.sidewaysFriction;
        if (isDrifting && grounded)
        {
            wheel.brakeTorque = acceleration / 4f;
            wheelSidewaysFriction.stiffness = driftFactor;
        }
        else
        {
            wheelSidewaysFriction.stiffness = 1 - (grounded ? driftTendency : 0);
        }
    }

// Actualizar la posición de las ruedas en el modelo visual.
    for (int i = 0; i < wheelMeshes.Length; i++)
    {
        Quaternion quat;
        Vector3 pos;
        wheels[i].GetWorldPose(out pos, out quat);

        wheelMeshes[i].position = pos;
        wheelMeshes[i].rotation = quat;
    }

// Verificar si el kart está en el aire.
    grounded = false;

    foreach (WheelCollider wheel in wheels)
    {
        if (wheel.isGrounded)
        {
            grounded = true;
            break;
        }
    }
}

}
