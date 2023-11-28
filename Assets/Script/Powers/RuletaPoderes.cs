using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuletaPoderes : MonoBehaviour
{
    public GameObject[] poderes;
    public GameObject[] poderesEvolucioneados;
    public KartEntity entity;

    public GameObject GirarRuleta()
    {
        if (entity.Coins >= 10)
        {
            int indicePoder = Random.Range(0, poderesEvolucioneados.Length);
            GameObject poderSeleccionado = poderesEvolucioneados[indicePoder];
            return poderSeleccionado;
        }
        else
        {
            int indicePoder = Random.Range(0, poderes.Length);
            GameObject poderSeleccionado = poderes[indicePoder];
            return poderSeleccionado;
        }
    }
}



