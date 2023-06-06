using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float slowDrag = 5f; // Valor de drag para ralentizar los objetos

    private void OnTriggerStay(Collider other)
    {
        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();

        if (otherRigidbody != null)
        {
            otherRigidbody.drag = slowDrag; // Aplica el valor de drag para ralentizar el objeto
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();

        if (otherRigidbody != null)
        {
            otherRigidbody.drag = 0f; // Restaura el valor de drag a cero para que los objetos vuelvan a su velocidad normal al salir de la zona de ralentización
        }
    }
}
