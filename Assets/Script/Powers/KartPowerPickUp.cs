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
    public GameObject missile;
    public GameObject campBullet;
    public Transform FrontPowerPos;
    public Transform BackPowerPos;
    public Transform BackPowerPos2;
    Vector3 destination;
    public Text powerText;
    private bool isSlowed;

    private float TimeSlowed;
    private float VelSlowed;
    private float BaseKarVel;
    [SerializeField] private KartController kart;

    public void Start()
    {
        BaseKarVel = kart.maxSpeed;

    }
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
    public void Slowed(bool isSlowed, float TimeSlow, float velocySlow)
    {
        this.isSlowed = isSlowed;
        TimeSlowed = TimeSlow;
        VelSlowed = velocySlow;
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

        }else if (selectedPower.CompareTag("Missile"))
        {
            GameObject _missile = Instantiate(missile, FrontPowerPos.position, FrontPowerPos.rotation);
            _missile.GetComponent<Missile>().Owner = this.gameObject;
        }
        else if (selectedPower.CompareTag("CampBullet"))
        {
            GameObject _bullet = Instantiate(campBullet, FrontPowerPos.position, FrontPowerPos.rotation);
            _bullet.GetComponent<CampBullet>().Owner = this.gameObject;
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

        if (isSlowed)
        {

            if (TimeSlowed > 0f)
            {
                TimeSlowed -= Time.deltaTime;
                kart.currentSpeed = VelSlowed;
            }
            else
            {
                kart.currentSpeed = BaseKarVel;

                isSlowed = false;
            }
        }
    }

    public void Op_UpdateUX()
    {
    }
}
