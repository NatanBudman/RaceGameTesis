using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartCamera : MonoBehaviour
{
    public Transform target; // Objeto objetivo a seguir (generalmente el coche del jugador)
    public Vector3 offset; // Desplazamiento de la cámara con respecto al objetivo
    public float smoothRotationSpeed = 5f; // Velocidad de suavizado para la rotación de la cámara

    private Vector3 desiredPosition; // Posición deseada de la cámara
    private Quaternion desiredRotation; // Rotación deseada de la cámara

    void LateUpdate()
    {
        // Calcula la posición deseada de la cámara teniendo en cuenta el desplazamiento y rotación del objetivo
        desiredPosition = target.position + offset;
        desiredRotation = Quaternion.LookRotation(target.forward);

        // Aplica la posición deseada a la cámara
        transform.position = desiredPosition;

        // Aplica la rotación deseada de forma suave usando la función Slerp
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothRotationSpeed * Time.deltaTime);
    }
}
