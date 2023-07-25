using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIController
{
    void DecideAction(GameObject kart, RuletaPoderes powerRoulette);
}

public class PowerController : MonoBehaviour, IAIController
{
    public void DecideAction(GameObject kart, RuletaPoderes powerRoulette)
    {
        KartPowerPickUp powerPickUp = kart.GetComponent<KartPowerPickUp>();

        // Simulamos una acción aleatoria solo si el kart de la IA no tiene un poder
        if (!powerPickUp.HasPower)
        {
            powerPickUp.SelectedPower = powerRoulette.GirarRuleta();
            powerPickUp.HasPower = true;
            Debug.Log(powerPickUp.SelectedPower);

            // Activar el poder seleccionado
            powerPickUp.ActivatePower();
            powerPickUp.HasPower = false;
        }
    }
}