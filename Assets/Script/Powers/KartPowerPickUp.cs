using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KartPowerPickUp : MonoBehaviour, IOptimizatedUpdate
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
            if (!hasPower)
            {
                // Girar la ruleta y obtener el poder seleccionado
                selectedPower = powerRoulette.GirarRuleta();

                hasPower = true;
            }
        }
    }

    private void ActivatePower()
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

    public void Op_UpdateGameplay()
    {
        if (hasPower && Input.GetKey(KeyCode.P))
        {
            Debug.Log("entre");
            ActivatePower();

            hasPower = false;
        }
    }

    public void Op_UpdateUX()
    {
    }
}
