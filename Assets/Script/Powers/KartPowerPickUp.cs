using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartPowerPickUp : MonoBehaviour
{
    public RuletaPoderes powerRoulette;
    private bool hasPower = false;
    private GameObject selectedPower;
    public GameObject risingWallPrefab;
    public GameObject mug;
    public Transform BackPowerPos;
    public Transform BackPowerPos2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerBox"))
        {
            Debug.Log("Toca");
            if (!hasPower)
            {
                // Girar la ruleta y obtener el poder seleccionado
                selectedPower = powerRoulette.GirarRuleta();

                hasPower = true;
            }
        }
    }

    private void Update()
    {
        if (hasPower && Input.GetKeyDown(KeyCode.P))
        {
            // Activar el poder seleccionado al presionar la tecla 'Espacio'
            ActivatePower();

            // Desactivar el poder
            hasPower = false;
        }
    }

    private void ActivatePower()
    {
        // Activar el poder seleccionado
        // Implementa la lógica para activar el poder específico seleccionado
        // Ejemplo:
        if (selectedPower.CompareTag("Power1"))
        {
            Instantiate(risingWallPrefab, BackPowerPos.position, BackPowerPos.rotation);
        }
        else if (selectedPower.CompareTag("Power2"))
        {
            Instantiate(mug, BackPowerPos2.position, BackPowerPos2.rotation);
        }
        // Añade más condiciones según los poderes que tengas

    }


}
