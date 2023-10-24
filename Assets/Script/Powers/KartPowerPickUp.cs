using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KartPowerPickUp : MonoBehaviour, IOptimizatedUpdate
{
    public PowerController aiController;
    public RuletaPoderes powerRoulette;
    public KartEntity entity;
    private bool hasPower = false;
    private bool availablePower = false;
    private GameObject selectedPower;
    public GameObject risingWallPrefab;
    public GameObject mug;
    public GameObject missile;
    public GameObject campBullet;
    public GameObject risingWallPrefab2;
    public GameObject mug2;
    public GameObject missile2;
    public GameObject campBullet2;
    public GameObject fakeObjectPrefab;
    public Transform FrontPowerPos;
    public Transform BackPowerPos;
    public Transform BackPowerPos2;
    public Animator ruletteAnimation;
    Vector3 destination;
    private bool isSlowed;

    private float TimeSlowed;
    private float VelSlowed;
    private float BaseKarVel;
    public float powerTimer = 10f;
    private float currentPowerTimer;
    private float currenPowerTimerIA;
    public int specialPower = 10;
    [SerializeField] private KartController kart;

    public Image iceWallImage;
    public Image mugImage;
    public Image missileImage;
    public Image campBulletImage;

    private Dictionary<string, Image> powerImages = new Dictionary<string, Image>();

    private float launchCooldown = 20f; 
    private float currentLaunchCooldown = 0.0f;
    private bool canLaunch = true;
    public Image fillImage;

    public void Start()
    {
        BaseKarVel = kart.maxSpeed;
        powerImages["IceWall"] = iceWallImage;
        powerImages["Mug"] = mugImage;
        powerImages["Missile"] = missileImage;
        powerImages["CampBullet"] = campBulletImage;
        currentPowerTimer = powerTimer;
        entity = GetComponent<KartEntity>();

    }

    private void UpdatePowerImage(bool isActive)
    {
        if (powerImages.TryGetValue(selectedPower.tag, out Image image))
        {
            image.gameObject.SetActive(isActive);
        }
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
    public bool AvailablePower
    {
        get { return availablePower; }
        set { availablePower = value; }


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

                    currentPowerTimer = powerTimer;
                    aiController.DecideAction(gameObject, powerRoulette);

                }
                else
                {
                    if (!availablePower)
                    {

                        // Si no hay un controlador de IA, el jugador toma la decisión
                        selectedPower = powerRoulette.GirarRuleta();
                        ruletteAnimation.SetBool("activarRuleta", true);
                        availablePower = true;
                    }
                }





            }
        }
    }

    public void PoderPlayer()
    {
        ruletteAnimation.SetBool("activarRuleta", false);

        hasPower = true;
        Debug.Log(selectedPower);
        UpdatePowerImage(true);
        currentPowerTimer = powerTimer;
        availablePower = false;
    }
    public void Slowed(bool isSlowed, float TimeSlow, float velocySlow)
    {
        this.isSlowed = isSlowed;
        TimeSlowed = TimeSlow;
        VelSlowed = velocySlow;
    }
    private void SpawnFakeObject()
    {
        // Asegúrate de que fakeObjectPrefab esté asignado en el Inspector
        if (fakeObjectPrefab != null)
        {


            GameObject fakeObject = Instantiate(fakeObjectPrefab, BackPowerPos2.position, BackPowerPos2.rotation);

        }
    }
        public void ActivatePower()
    {
      
            if (selectedPower.CompareTag("IceWall2"))
            {
                GameObject _risingWallPrefab = Instantiate(risingWallPrefab2, BackPowerPos.position, BackPowerPos.rotation);
                _risingWallPrefab.GetComponent<IceWall>().Owner = this.gameObject;
                entity.Coins -= 10;
            }
            else if (selectedPower.CompareTag("Mug2"))
            {
                GameObject _mug = Instantiate(mug2, BackPowerPos2.position, BackPowerPos2.rotation);
                _mug.GetComponent<SlowZone>().Owner = this.gameObject;
                entity.Coins -= 10;


            }
            else if (selectedPower.CompareTag("Missile2"))
            {
                GameObject _missile = Instantiate(missile2, FrontPowerPos.position, FrontPowerPos.rotation);
                _missile.GetComponent<Missile>().Owner = this.gameObject;
                entity.Coins -= 10;

            }
            else if (selectedPower.CompareTag("CampBullet2"))
            {
                GameObject _bullet = Instantiate(campBullet2, FrontPowerPos.position, FrontPowerPos.rotation);
                _bullet.GetComponent<CampBullet>().Owner = this.gameObject;
                entity.Coins -= 10;

            }
        
       
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
            else if (selectedPower.CompareTag("Missile"))
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


    public void Op_UpdateGameplay()
    {
        if (aiController == null && hasPower && Input.GetKey(KeyCode.F))
        {
            ActivatePower();
            hasPower = false;
            UpdatePowerImage(false);
        }
        if (availablePower)
        {
            currentPowerTimer -= Time.deltaTime;
            currenPowerTimerIA -= Time.deltaTime;
        }
        if (currentPowerTimer <= 0)
        {

            PoderPlayer();

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
        if (aiController != null)
        {
            if (hasPower)
            {

                if (aiController.CheckRange(aiController.target) && aiController.CheckAngle(aiController.target) && aiController.CheckView(aiController.target) && (SelectedPower.CompareTag("Missile") || SelectedPower.CompareTag("CampBullet") || SelectedPower.CompareTag("Missile2") || SelectedPower.CompareTag("CampBullet2")))
                {
                    ActivatePower();
                    HasPower = false;
                    Debug.Log("PoderAdelante");
                }
                if (aiController.CheckRangeBack(aiController.target) && aiController.CheckAngleBack(aiController.target) && aiController.CheckViewBack(aiController.target) && (SelectedPower.CompareTag("Mug") || SelectedPower.CompareTag("IceWall") || SelectedPower.CompareTag("Mug2") || SelectedPower.CompareTag("IceWall2")))
                {
                    ActivatePower();
                    HasPower = false;
                    Debug.Log("PoderAtras");

                }
            }


        }
        if (!canLaunch)
        {
            currentLaunchCooldown -= Time.deltaTime;

            if (currentLaunchCooldown <= 0.0f)
            {
                canLaunch = true;
                currentLaunchCooldown = 0.0f;
            }
            else
            {
                float fillAmount = 1.0f - (currentLaunchCooldown / launchCooldown);
                fillImage.fillAmount = fillAmount;
            }
        }

        // Verifica si se presionó la tecla "G" y el temporizador termino
        if (aiController == null && Input.GetKeyDown(KeyCode.G) && canLaunch)
        {
            SpawnFakeObject();
            canLaunch = false;
            fillImage.fillAmount = 0.0f;
            currentLaunchCooldown = launchCooldown;
        }
    }

    public void Op_UpdateUX()
    {
    }
}