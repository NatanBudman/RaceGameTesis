using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private KartEntity kartEntity;


    #region Stats

    private float SpeedStats => kartEntity.GetMaxRealSpeed;

    private float SteerDirStats => kartEntity.GetMaxRealSpeedRotate;

    #endregion
    
    [Space]
    [Space]

    #region Paramets

        [HideInInspector] public float currentSpeed = 0;
        [SerializeField] private float steerAmount;
        [SerializeField] private float steerDirection;
        [SerializeField] private float gravity;
        [SerializeField] private float jumpForce;
        [SerializeField] private float deaccelerationTime;
        private float driftTime;
        public float minSpeedToCrash;
        public float realSpeed; //not the applied speed
        public float maxSpeed => SpeedStats; //max possible speed

        public float boostSpeed; //speed while boosting
        public Vector3 jumpDirection = new Vector3(0,0,1); //direction in which the kart will jump, strictly up for now
    
        [SerializeField] private float outwardDriftForce;
        private bool driftLeft;
        private bool driftRight;

    #endregion
    
    // fuerzaDeChoque
  

    public bool isGrounded;
    public bool hasInputManager;

    [SerializeField] public Rigidbody rb;
    [HideInInspector] public float StartVelocity;

    private bool Choco = false;
    private void Start()
    {
        hasInputManager = _inputManager;
    }


    private void FixedUpdate()
    {
        Drive();
        if (_inputManager != null) 
        {
            Steering();
        }
        GroundNormalRotation();
        Drift(); 
    }
    

    private float velocit;
    public void Drive()
    {
        if (!Choco)
        {
            realSpeed = transform.InverseTransformDirection(rb.velocity).z; 
        }
        
        if (hasInputManager) //Controls for players
        {
            if (Input.GetKey(_inputManager.MovForward) && isGrounded )
            {
                currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * 0.5f); //lerp=(el valor de interpolacion, el valor al que quiero llegar, velocidad de interpolacion/aceleracion)

            }
            else if (Input.GetKey(_inputManager.MovReverse) && isGrounded )
            {
                currentSpeed = Mathf.Lerp(currentSpeed, -maxSpeed / 1.75f, Time.deltaTime * 1f);
            }
            else 
            {
                if (isGrounded)
                {
                    currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * deaccelerationTime); //reduccion de velocidad a 0 en el piso si no acelera ni retrocede
                }
                else
                {
                    currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * deaccelerationTime); //reduccion de velocidad a 0 en el aire si no acelera ni retrocede
                    rb.AddForce(Vector3.down * gravity, ForceMode.Impulse);
                }

            }
        }
        else //Controls for Bots
        {
           
            if (isGrounded)
            {
                currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * 0.5f);
                //lerp=(el valor de interpolacion, el valor al que quiero llegar, velocidad de interpolacion/aceleracion)
            }
            else if(!isGrounded)
            {
                rb.AddForce(Vector3.down * gravity,ForceMode.Impulse);
            }
        }

        Vector3 vel = transform.forward * currentSpeed;
        vel.y = rb.velocity.y; //gravity setting
        rb.velocity = vel;
    }

    private void Steering()
    {
        steerDirection = Input.GetAxisRaw("Horizontal");
        Vector3 steerDirVector; //se usa para la rotacion final del kart al doblar
        
        outwardDriftForce = 10000 * (currentSpeed / 10f);
        
        if (driftLeft && !driftRight) //drift a izq
        {
            steerDirection = Input.GetAxis("Horizontal") < 0 ? -1.5f : -0.5f;
            
            //_turboSystem.GetTurbo();
            
            if (isGrounded)
            {
                rb.AddForce(transform.right * (outwardDriftForce * Time.deltaTime), ForceMode.Acceleration);
            }
        }
        else if (driftRight && !driftLeft) //drift a der
        {
            steerDirection = Input.GetAxis("Horizontal") > 0 ? 1.5f : 0.5f;
            //_turboSystem.GetTurbo();
            if (isGrounded)
            {
                rb.AddForce(transform.right * (-outwardDriftForce * Time.deltaTime), ForceMode.Acceleration);
            }
        }

       
        //como la direccion del auto es mas fuerte al ir a velocidades mas bajas, ajustamos steerAmount en base a la velocidad real del kart y luego rotamos el kart sobre su eje con steerAmount
        // los numeros 4 y 1.5 pueden requerir ajustes segun la maniobrabilidad del kart

        if ((realSpeed > SpeedStats / 2))
        {
            steerAmount = (realSpeed / 1.5f * steerDirection) * SteerDirStats;
        }
        else if ((realSpeed <= SpeedStats / 2  && realSpeed > SpeedStats / 4) || (realSpeed < -(SpeedStats / 4)))
        {
            steerAmount = (realSpeed / 1.25f * steerDirection) * SteerDirStats;
        }
        else if ((realSpeed <= SpeedStats / 4 && realSpeed > SpeedStats / 6) || (realSpeed < -(SpeedStats / 6) && realSpeed >= -(SpeedStats / 4)))
        {
            steerAmount = (realSpeed * steerDirection) * SteerDirStats;
        }
        else if ((realSpeed <= SpeedStats / 6 && realSpeed > 0.01f) || (realSpeed < -1f))
        {
            steerAmount = ((realSpeed * steerDirection * 0.5f) * SteerDirStats) * 2.5f;
        }
        else if ((realSpeed < 1f) || (realSpeed > -1f))
        {
            steerAmount = 0;
        }

        steerDirVector = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + steerAmount, transform.eulerAngles.z);

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, steerDirVector, 1.5f * Time.deltaTime);
    }

    private void GroundNormalRotation()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1f))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up * 2, hit.normal) * transform.rotation, 7.5f * Time.deltaTime);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    
    private void Drift()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            if (steerDirection > 0)
            {
                driftRight = true;
                driftLeft = false;
            }
            else if (steerDirection < 0)
            {
                driftLeft = true;
                driftRight = false;
            }
        }

        if (!Input.GetKey(KeyCode.Space))
        {
            driftLeft = false;
            driftRight = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.gameObject.layer == 6)
        {
            Choco = true;
            
            // Obtener la normal de la colisión para obtener la dirección de rebote
            Vector3 direccionRebote = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            realSpeed -= 10;
            // Aplicar una fuerza en la dirección de rebote
            rb.AddForce(direccionRebote * realSpeed, ForceMode.Impulse);

            
        }
    }


    private void OnCollisionExit(Collision other)
    {
        Choco = false;
    }


    private void Crash(GameObject collision)
        {
            Vector3 posicionAnterior = transform.position;
            Vector3 posicionActual = collision.gameObject.transform.position;

            Vector3 distanciaMovida = posicionActual - posicionAnterior;
            float distanciaX = Mathf.Abs(distanciaMovida.x);
            float distanciaY = Mathf.Abs(distanciaMovida.y);
            float distanciaZ = Mathf.Abs(distanciaMovida.z);

            if (distanciaX >= distanciaY && distanciaX >= distanciaZ)
            {
                // El objeto ha chocado por el lado derecho o izquierdo
                if (distanciaMovida.x >= 0)
                {
                    // El objeto ha chocado por el lado derecho
                    transform.Rotate(new Vector3(0, transform.forward.y - 25, 0));
                }
                else
                {
                    // El objeto ha chocado por el lado izquierdo
                    transform.Rotate(new Vector3(0, transform.forward.y + 25, 0));
                }
            }
            bool haChocadoPorDerecha = (distanciaMovida.x >= 0);
            Debug.Log(haChocadoPorDerecha);
        }

    private void Crash2(GameObject collision)
        {
       
        }

      
        
        
}
