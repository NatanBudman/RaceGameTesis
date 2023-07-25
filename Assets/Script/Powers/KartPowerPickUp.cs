using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KartPowerPickUp : MonoBehaviour, IOptimizatedUpdate
{
    public PowerController aiController;
    public RuletaPoderes powerRoulette;
    private bool hasPower = false;
    private GameObject selectedPower;
    public GameObject risingWallPrefab;
    public GameObject mug;
    public Transform BackPowerPos;
    public Transform BackPowerPos2;
    Vector3 destination;
    public Text powerText;
    public GameObject SelectedPower
    {
        get { return selectedPower; }
        set { selectedPower = value; }
    }

    // Propiedad para verificar si el kart tiene un poder
    public bool HasPower
    {
        get { return hasPower; }
        set { hasPower = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerBox"))
        {
            if (!hasPower)
            {
                if (aiController != null)
                {
                    // Si hay un controlador de IA, dejar que tome la decisión
                    Debug.Log("poderIA");

                    aiController.DecideAction(gameObject, powerRoulette);
                }
                else
                {
                    // Si no hay un controlador de IA, el jugador toma la decisión
                    selectedPower = powerRoulette.GirarRuleta();
                    hasPower = true;
                    Debug.Log(selectedPower);
                    UpdatePowerText();
                }
            }
        }
    }

    public void ActivatePower()
    {
        if (selectedPower.CompareTag("IceWall"))
        {
            GameObject _risingWallPrefab = Instantiate(risingWallPrefab, BackPowerPos.position, BackPowerPos.rotation);
            _risingWallPrefab.GetComponent<IceWall>().Owner = this.gameObject;
        }
        else if (selectedPower.CompareTag("Mug"))
        {
            GameObject _mug = Instantiate(mug, BackPowerPos2.position, BackPowerPos2.rotation);
            _mug.GetComponent<SlowZone>().Owner = this.gameObject;
        }
      
    }
    private void UpdatePowerText()
    {
        if (powerText != null)
        {
            powerText.text = selectedPower.name;
        }
    }
    public void Op_UpdateGameplay()
    {
        if (hasPower && Input.GetKey(KeyCode.F))
        {
            ActivatePower();
            hasPower = false;
            powerText.text = "";
        }
    }

    public void Op_UpdateUX()
    {
    }
}
