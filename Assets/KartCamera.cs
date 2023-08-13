using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartCamera : MonoBehaviour
{
    public Transform target; // Objeto objetivo a seguir (generalmente el coche del jugador)
    public Vector3 offset; // Desplazamiento de la c�mara con respecto al objetivo
    public float smoothRotationSpeed = 5f; // Velocidad de suavizado para la rotaci�n de la c�mara

    private Vector3 desiredPosition; // Posici�n deseada de la c�mara
    private Quaternion desiredRotation; // Rotaci�n deseada de la c�mara

    void LateUpdate()
    {
        // Calcula la posici�n deseada de la c�mara teniendo en cuenta el desplazamiento y rotaci�n del objetivo
        desiredPosition = target.position + offset;
        desiredRotation = Quaternion.LookRotation(target.forward);

        // Aplica la posici�n deseada a la c�mara
        transform.position = desiredPosition;

        // Aplica la rotaci�n deseada de forma suave usando la funci�n Slerp
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothRotationSpeed * Time.deltaTime);
    }
}
