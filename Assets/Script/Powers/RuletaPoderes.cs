using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuletaPoderes : MonoBehaviour
{

    public GameObject[] poderes;

    public GameObject GirarRuleta()
    {
        // Detener la ruleta y seleccionar un poder al azar
        int indicePoder = Random.Range(0, poderes.Length);
        GameObject poderSeleccionado = poderes[indicePoder];

        Debug.Log("Agarro");
        // Devolver el poder seleccionado
        return poderSeleccionado;
    }
}



